using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ClientServerModels.GameSessionModels;

namespace Authorization.Models.GameSessionModels
{
    public class GameObjectModel
    {
        public double CanvasTop { get; set; } 
        public double CanvasLeft { get; set; }
        public Ellipse View { get; }
        public GameObjectModel(GameObject gameObject, Camera camera)
        {

            View = new Ellipse()
            {
                Height = gameObject.Radius * 2,
                Width = gameObject.Radius * 2,
                Fill =  new SolidColorBrush(Color.FromRgb(
                    gameObject.MainRedColor,
                    gameObject.MainGreenColor,
                    gameObject.MainBlueColor)),
                Stroke = new SolidColorBrush(Color.FromRgb(
                    gameObject.StrokeRedColor,
                    gameObject.StrokeGreenColor,
                    gameObject.StrokeBlueColor
                    )),
                StrokeDashArray = new DoubleCollection() { gameObject.StrokeLength, gameObject.StrokeSpace },
                StrokeThickness = gameObject.StrokeThickness,
            };
            
            // Start coordinates
            Move(gameObject.GlobalX - camera.GlobalX - gameObject.Radius,
                gameObject.GlobalY - camera.GlobalY - gameObject.Radius);
        }

        public void Move(double left, double top)
        {
            CanvasLeft += left;
            CanvasTop  += top;
            Canvas.SetLeft(View, CanvasLeft);
            Canvas.SetTop (View, CanvasTop);
        }
        
    }
}