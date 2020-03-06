using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlickoRank.Data;
using GlickoRank.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using GlickoRank.Helpers;

namespace GlickoRank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly MvcGlickoRankContext _context;

        public CharactersController(MvcGlickoRankContext context)
        {
            _context = context;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacter()
        {
            return await _context.Character.ToListAsync();
        }

        private IList<Activity> GetCharacterActivities(int id, DateTime startDate)
        {
            Character character = _context.Character.Include(c => c.CharacterActivities).SingleOrDefault(c => c.ID == id);
            if (character != null)
            {
                int i = 0;
                IList<Activity> activities = new List<Activity>();
                bool breakWhile = false;
                do
                {
                    string responseFromServerActivities = ApiHelper.GetRequest($"https://www.bungie.net/Platform/Destiny2/{character.MembershipType}/Account/{character.MembershipId}/Character/{character.CharacterId}/Stats/Activities/?mode=19&count=5&page={i}");
                    if (responseFromServerActivities != null)
                    {
                        JObject jsonResponseActivities = JObject.Parse(responseFromServerActivities);

                        IList<JToken> resultsActivities = jsonResponseActivities["Response"]["activities"]?.Children().ToList();
                        if (resultsActivities == null)
                        {
                            break;
                        }
                        foreach (JToken tokenActivity in resultsActivities)
                        {
                            BungieAPI.CharacterActivity APIactivity = tokenActivity.ToObject<BungieAPI.CharacterActivity>();
                            Activity activity = _context.Activity.SingleOrDefault(a => a.InstanceId == APIactivity.activityDetails.instanceId);
                            if (activity == null)
                            {
                                activity = new Activity()
                                {
                                    InstanceId = APIactivity.activityDetails.instanceId,
                                    Period = APIactivity.period,
                                    Mode = 19
                                };
                            }

                            if (activity.Period < startDate)
                            {
                                breakWhile = true;
                                break;
                            }

                            activities.Add(activity);
                        }
                        i++;
                    }
                } while (activities.Count > 0 && !breakWhile);
                return activities;
            }
            return null;
        }

        [HttpGet]
        [Route("GetAssociatedPlayers/{id}")]
        public async Task<ActionResult<int>> GetAssociatedPlayers(int id)
        {
            int newPlayersCount = 0;
            Character character = _context.Character.Find(id);

            IList<Activity> activities = GetCharacterActivities(character.ID, new DateTime(2019, 12, 10));
            foreach (Activity activity in activities)
            {
                if (activity.ID == 0)
                {
                    _context.Activity.Add(activity);
                    _context.SaveChanges();
                }

                /////////////////////////

                string responseFromServerCharacters = ApiHelper.GetRequest($"http://stats.bungie.net/Platform/Destiny2/Stats/PostGameCarnageReport/{activity.InstanceId}/");

                JObject jsonResponseCharacters = JObject.Parse(responseFromServerCharacters);

                BungieAPI.ActivityReport APIreport = jsonResponseCharacters["Response"].ToObject<BungieAPI.ActivityReport>();
                foreach (BungieAPI.Entry entry in APIreport.entries)
                {
                    Character newCharacter = _context.Character.Include(c => c.CharacterActivities).SingleOrDefault(c => c.CharacterId == entry.characterId);
                    if (newCharacter == null)
                    {
                        newCharacter = new Character
                        {
                            Name = $"{entry.player.destinyUserInfo.displayName}_{entry.player.destinyUserInfo.membershipType}_{entry.characterId}",
                            CharacterId = entry.characterId,
                            MembershipId = entry.player.destinyUserInfo.membershipId,
                            MembershipType = entry.player.destinyUserInfo.membershipType,
                            CharacterActivities = new List<CharacterActivity>()
                    };
                        _context.Character.Add(newCharacter);
                        newPlayersCount++;
                        //Console.WriteLine($"[{++newCharacterCount}] Add Character: {newCharacter.Name}");
                        _context.SaveChanges();
                    }

                    if (newCharacter.CharacterActivities.SingleOrDefault(ca => ca.ActivityID == activity.ID) == null)
                    {
                        CharacterActivity newCharacterActivity = new CharacterActivity
                        {
                            Activity = activity,
                            Score = entry.score.basic.value,
                            Team = entry.values.team.basic.value,
                            TeamScore = entry.values.teamScore.basic.value,
                            Standing = entry.standing,
                            Completed = entry.values.completed.basic.value > 0 ? true : false
                    };

                    newCharacter.CharacterActivities.Add(newCharacterActivity);
                    _context.SaveChanges();
                }
            }
        }
            return newPlayersCount;
        }

    [HttpGet]
    [Route("GetRepeatedPlayers")]
    public ActionResult<int> GetRepeatedPlayers()
    {
        return _context.Character.Include(c => c.CharacterActivities).Where(c => c.CharacterActivities.Count > 1).Count();
    }

    // GET: api/Characters/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Character>> GetCharacter(int id)
    {
        var character = await _context.Character.FindAsync(id);

        if (character == null)
        {
            return NotFound();
        }

        return character;
    }

    // PUT: api/Characters/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCharacter(int id, Character character)
    {
        if (id != character.ID)
        {
            return BadRequest();
        }

        _context.Entry(character).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CharacterExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Characters
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Character>> PostCharacter(Character character)
    {
        _context.Character.Add(character);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCharacter", new { id = character.ID }, character);
    }

    // DELETE: api/Characters/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Character>> DeleteCharacter(int id)
    {
        var character = await _context.Character.FindAsync(id);
        if (character == null)
        {
            return NotFound();
        }

        _context.Character.Remove(character);
        await _context.SaveChangesAsync();

        return character;
    }

    private bool CharacterExists(int id)
    {
        return _context.Character.Any(e => e.ID == id);
    }
}
}
