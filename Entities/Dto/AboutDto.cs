using Core.Entity.Abstract;

namespace Entities.Dto
{
	public class AboutDto : IDto
	{
        public int Id { get; set; }
        public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public bool IsDelete { get; set; }

	}
}
