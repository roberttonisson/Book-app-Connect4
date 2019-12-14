using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameEngine
{
    public class GameSettings
    {
        public string GameName { get; set; } = "Connect 4";
        [Display(Name = "Board Height")]
        public int BoardHeight { get; set; } = 4;
        [Display(Name = "Board Width")]
        public int BoardWidth { get; set; } = 4;
        
    }
}