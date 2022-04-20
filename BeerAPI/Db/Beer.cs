namespace BeerAPI.Db
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double? Rating { get; set; }
        public int RatingCounter { get; set; }
    }
}
