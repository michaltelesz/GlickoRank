using System;

namespace BungieAPI
{
    public class ActivityReport
    {
        public DateTime period { get; set; }
        public int startingPhaseIndex { get; set; }
        public ActivityDetails activityDetails { get; set; }
        public Entry[] entries { get; set; }
        public Team[] teams { get; set; }
    }

    public class Entry
    {
        public int standing { get; set; }
        public Score score { get; set; }
        public Player player { get; set; }
        public string characterId { get; set; }
        public Values values { get; set; }
        //public Extended extended { get; set; }
    }

    public class Score
    {
        public Basic basic { get; set; }
    }

    public class Player
    {
        public DestinyUserInfo destinyUserInfo { get; set; }
        public string characterClass { get; set; }
        public long classHash { get; set; }
        public long raceHash { get; set; }
        public long genderHash { get; set; }
        public int characterLevel { get; set; }
        public int lightLevel { get; set; }
        public long emblemHash { get; set; }
    }

    public class DestinyUserInfo
    {
        public string iconPath { get; set; }
        public int crossSaveOverride { get; set; }
        public int[] applicableMembershipTypes { get; set; }
        public bool isPublic { get; set; }
        public int membershipType { get; set; }
        public string membershipId { get; set; }
        public string displayName { get; set; }
    }

    public class Team
    {
        public int teamId { get; set; }
        public Stat standing { get; set; }
        public Score score { get; set; }
        public string teamName { get; set; }
    }
}