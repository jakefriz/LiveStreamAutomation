using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace LiveStreamAutomation
{
    public class Team : IEquatable<Team> //, IComparable<Team>
    {
        [Key]
        public string FullTeamString { get; set; }
        public TeamStates State { get; set; }
        public string Event { get; set; }
        public string Division { get; set; }
        public string Pool { get; set; }
        public string Round { get; set; }
        public string TeamName { get; set; } // Optional, in case the team has a name
        public int TeamNumber { get; set; } // Play Order Number
        public List<Player> Players { get; set; }

        public float ArtisticImpressionScore { get; set; }
        public float ExecutionScore { get; set; }
        public float DifficultyScore { get; set; }

        public Team()
        {
            Players = new List<Player>();
        }

		// Only care about matching same players and Pool
		public bool Equals(Team other)
		{
			if (Event != other.Event ||
				Division != other.Division ||
				Pool != other.Pool ||
				Round != other.Round ||
				Players.Count != other.Players.Count)
			{
				return false;
			}

			foreach (Player player in Players)
			{
				bool bFound = false;
				foreach (Player otherPlayer in other.Players)
				{
					if (player.ValueEquals(otherPlayer))
					{
						bFound = true;
						break;
					}
				}

				if (!bFound)
				{
					return false;
				}
			}

			return true;
		}

        //public int CompareTo(Team other)
        //{
        //    if (other == null)
        //    {
        //        return 1;
        //    }
        //    return this.TeamNumber.CompareTo(other.TeamNumber);
        //}
    }

    public enum TeamStates
    {
        None,
		Inited,
        JudgesReady,
        Begin,
        Stopped,
        Finished,
        ScoresRecorded
    }

    public class Player : IComparable<Player>
    {
        [Key]
        public string Name { get; set; }
        public int Rank { get; set; }
        public string HomeCity { get; set; }
        public string HomeCountry { get; set; }

        public bool ValueEquals(Player other)
		{
			return Name == other.Name && Rank == other.Rank && HomeCity == other.HomeCity && HomeCountry == other.HomeCountry;
		}

        public int CompareTo(Player other)
        {
            if (other == null)
            {
                return 1;
            }
            return this.Name.CompareTo(other.Name);
        }

    }

	public class TeamList
	{
		public ObservableCollection<Team> Teams = new ObservableCollection<Team>();

		public void UpdateTeam(Team team)
		{
			int teamIndex = Teams.IndexOf(team);
			if (teamIndex == -1)
			{
				Teams.Add(team);
			}
			else
			{
				Teams[teamIndex] = team;
			}
		}
	}
}
