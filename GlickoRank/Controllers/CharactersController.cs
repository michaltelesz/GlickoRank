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

        [HttpGet]
        [Route("GetAllAssociatedPlayers")]
        public async Task<ActionResult<int>> GetAllAssociatedPlayers()
        {
            Console.WriteLine("TEST");
            List<Character> characters = await _context.Character.ToListAsync();

            int newActivityCount = 0;
            int newCharacterCount = 0;

            GlickoPlayer glickoPlayer = new GlickoPlayer { Rating = 1500, RD = 200, Volatility = 0.06f };
            GlickoResult[] glickoResults = {
                new GlickoResult{ Player = new GlickoPlayer { Rating = 1400, RD = 30 }, Result = 1 },
                new GlickoResult{ Player = new GlickoPlayer { Rating = 1550, RD = 100}, Result = 0 },
                new GlickoResult{ Player = new GlickoPlayer { Rating = 1700, RD = 300}, Result = 0 }
            };
            GlickoHelper.CalculateRank(glickoPlayer, glickoResults);

            foreach (Character character in characters)
            {
                Console.WriteLine($"CHARACTER [{character.ID}]");

                string responseFromServerActivities = ApiHelper.GetRequest($"https://www.bungie.net/Platform/Destiny2/{character.MembershipType}/Account/{character.MembershipId}/Character/{character.CharacterId}/Stats/Activities/?mode=19&count=10");
                JObject jsonResponseActivities = JObject.Parse(responseFromServerActivities);

                IList<JToken> resultsActivities = jsonResponseActivities["Response"]["activities"].Children().ToList();
                foreach (JToken tokenActivity in resultsActivities)
                {
                    BungieAPI.CharacterActivity APIactivity = tokenActivity.ToObject<BungieAPI.CharacterActivity>();
                    Activity newActivity = _context.Activity.SingleOrDefault(a => a.InstanceId == APIactivity.activityDetails.instanceId);
                    if (newActivity == null)
                    {
                        newActivity = new Activity()
                        {
                            InstanceId = APIactivity.activityDetails.instanceId,
                            Period = APIactivity.period
                        };
                        _context.Activity.Add(newActivity);
                        Console.WriteLine($"[{++newActivityCount}] Add Activity: {newActivity.InstanceId} from {newActivity.Period}");
                        _context.SaveChanges();

                        /////////////////////////
                        ///

                        string responseFromServerCharacters = ApiHelper.GetRequest($"http://stats.bungie.net/Platform/Destiny2/Stats/PostGameCarnageReport/{newActivity.InstanceId}/");

                        JObject jsonResponseCharacters = JObject.Parse(responseFromServerCharacters);

                        BungieAPI.ActivityReport APIcharacter = jsonResponseCharacters["Response"].ToObject<BungieAPI.ActivityReport>();
                        foreach (BungieAPI.Entry entry in APIcharacter.entries)
                        {
                            Character newCharacter = _context.Character.Include(c => c.CharacterActivities).SingleOrDefault(c => c.CharacterId == entry.characterId);
                            if (newCharacter == null)
                            {
                                newCharacter = new Character
                                {
                                    Name = $"{entry.player.destinyUserInfo.displayName}_{entry.player.destinyUserInfo.membershipType}_{entry.characterId}",
                                    CharacterId = entry.characterId,
                                    MembershipId = entry.player.destinyUserInfo.membershipId,
                                    MembershipType = entry.player.destinyUserInfo.membershipType
                                };
                                _context.Character.Add(newCharacter);
                                //Console.WriteLine($"[{++newCharacterCount}] Add Character: {newCharacter.Name}");
                                _context.SaveChanges();
                            }


                            if (newCharacter.CharacterActivities == null)
                            {
                                newCharacter.CharacterActivities = new List<CharacterActivity>();
                            }
                            if (!newCharacter.CharacterActivities.Any(ca => ca.ActivityId == newActivity.ID))
                            {
                                CharacterActivity newCharacterActivity = new CharacterActivity
                                {
                                    Activity = newActivity,
                                    Character = newCharacter
                                };

                                newCharacter.CharacterActivities.Add(newCharacterActivity);
                                _context.SaveChanges();
                            }
                        }
                        _context.SaveChanges();
                    }
                }
            }
            return newActivityCount;
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
