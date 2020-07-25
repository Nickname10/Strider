using ClientServerModels.GameSessionModels;

namespace Authorization.Models.GameSessionModels
{
    public class Camera
    {
        // Left top coordinates
        public double GlobalX { get; private set; }
        public double GlobalY { get; private set; }
        //can be scale but not now
        private readonly double _canvasCenterX;
        private readonly double _canvasCenterY;
        public Camera(GameObject player, double canvasWidth, double canvasHeight)
        {
             _canvasCenterX = canvasWidth / 2;
             _canvasCenterY = canvasHeight / 2;
            GlobalX = player.GlobalX - _canvasCenterX;
            GlobalY = player.GlobalY - _canvasCenterY; 
        }

        public void ChangeCoordinates(GameObject player)
        {
            GlobalX = player.GlobalX - _canvasCenterX;
            GlobalY = player.GlobalY - _canvasCenterY;
        }
    }
}