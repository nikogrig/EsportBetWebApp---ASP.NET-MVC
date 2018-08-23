using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraBet.Data.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Game { get; set; }

        [Required]
        public string Legue { get; set; }

        [Required]
        public bool IsLive { get; set; }

        public int CategoryId { get; set; } //CategoryGameId

        public int SportId { get; set; }

        public Sport Sport { get; set; }

        public ICollection<Match> Matches { get; set; } = new HashSet<Match>();
    }
}
