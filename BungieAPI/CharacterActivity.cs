using System;

namespace BungieAPI
{
    public class CharacterActivity
    {
        public DateTime period { get; set; }
        public ActivityDetails activityDetails { get; set; }
        public Values values { get; set; }
    }

    public class ActivityDetails
    {
        public long referenceId { get; set; }
        public long directorActivityHash { get; set; }
        public string instanceId { get; set; }
        public int mode { get; set; }
        public int[] modes { get; set; }
        public bool isPrivate { get; set; }
        public int membershipType { get; set; }
    }

    public class Values
    {
        public Stat assists { get; set; }
        public Stat score { get; set; }
        public Stat kills { get; set; }
        public Stat averageScorePerKill { get; set; }
        public Stat deaths { get; set; }
        public Stat averageScorePerLife { get; set; }
        public Stat completed { get; set; }
        public Stat opponentsDefeated { get; set; }
        public Stat efficiency { get; set; }
        public Stat killsDeathsRatio { get; set; }
        public Stat killsDeathsAssists { get; set; }
        public Stat activityDurationSeconds { get; set; }
        public Stat standing { get; set; }
        public Stat team { get; set; }
        public Stat completionReason { get; set; }
        public Stat fireteamId { get; set; }
        public Stat startSeconds { get; set; }
        public Stat timePlayedSeconds { get; set; }
        public Stat playerCount { get; set; }
        public Stat teamScore { get; set; }
    }

    public class Messagedata
    {
    }
}