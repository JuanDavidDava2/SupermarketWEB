using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketWEB.Models
{
	public class Product
	{
		// [Key] -> Anotacion si la propiedad co se llama id. ejemplo productis

		public int Id { get; set; } //sera la llave primaria

		public string Name { get; set; }

		[Column(TypeName = "decimal(6,2)")]

		public decimal Price { get; set; }
		public int Stock { get; set; }

		public int CategoryId { get; set; } //Sera la llave foranea

		public Category? Category { get; set; } = default!;// propiedad de navegacion 
	}
}