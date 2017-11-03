using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GoldDuck.Models
{
    public class Idea : BaseEntity
    {
        [Key]
        public int id {get; set;}
        public string body {get; set;}
        public List<Like> likes {get; set;}
        public int users_id {get; set;}
        [ForeignKey("users_id")]
        public User user {get; set;}
        public DateTime created_at {get; set;}
        public DateTime updated_at {get; set;}

        public Idea()
        {
            likes = new List<Like>();
        }
    }
}