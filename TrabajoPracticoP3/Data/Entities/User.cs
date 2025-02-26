using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrabajoPracticoP3.Data.Enum;
using TrabajoPracticoP3.Data.Entities;


namespace TrabajoPracticoP3.Data.Entities
{
    public class User
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? Email { get; set; }
        public UserType UserType { get; set; }
        public string? Adress { get; set; }
       

        [Required]
        public string? UserName { get; set; }
        
        public string? Password { get; set; }
        public StateUser UserState { get; set; } = StateUser.Enabled;




    }



}