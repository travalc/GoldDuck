using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GoldDuck.Models
{
    public class Like : BaseEntity
    {
        [Key]
        public int id {get; set;}
        public int users_id {get; set;}
        [ForeignKey("users_id")]
        public User user {get; set;}
        public int ideas_id {get; set;}
        [ForeignKey("ideas_id")]
        public Idea idea {get; set;}
        public DateTime created_at {get; set;}
        public DateTime updated_at {get; set;}
    }
}