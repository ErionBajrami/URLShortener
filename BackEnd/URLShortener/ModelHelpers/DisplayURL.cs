﻿namespace URLShortener.ModelHelpers
{
        public class DisplayURL
        {
            public int Id { get; set; }
            public string OriginalUrl { get; set; }
            public string ShortUrl { get; set; }
            public int NrOfClicks { get; set; }
            public int? UserId { get; set; }
        }
  
}
