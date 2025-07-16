using System;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;

public class DrawingFunctions
{
    private Image<Bgr, byte> imgForward;
    private Image<Bgr, byte> imgReverse;
    private Image<Bgr, byte> imgLeft;
    private Image<Bgr, byte> imgRight;
    private Image<Bgr, byte> imgStop;

    public DrawingFunctions()
    {
        imgForward = new Image<Bgr, byte>("gesture_detector/resources/images/forward.png");
        imgReverse = new Image<Bgr, byte>("gesture_detector/resources/images/reverse.png");
        imgLeft = new Image<Bgr, byte>("gesture_detector/resources/images/left.png");
        imgRight = new Image<Bgr, byte>("gesture_detector/resources/images/right.png");
        imgStop = new Image<Bgr, byte>("gesture_detector/resources/images/stop.png");
    }

    public Image<Bgr, byte> DrawImage(Image<Bgr, byte> originalFrame, Image<Bgr, byte> actionImage)
    {
        int al = actionImage.Height;
        int an = actionImage.Width;

        // Verifica límites para evitar errores de índice
        int yStart = 600;
        int xStart = 50;
        int yEnd = yStart + al;
        int xEnd = xStart + an;

        if (yEnd > originalFrame.Height) yEnd = originalFrame.Height;
        if (xEnd > originalFrame.Width) xEnd = originalFrame.Width;

        // Copia la imagen de acción sobre el frame original
        for (int y = yStart; y < yEnd; y++)
        {
            for (int x = xStart; x < xEnd; x++)
            {
                var pixel = actionImage[y - yStart, x - xStart];
                originalFrame[y, x] = pixel;
            }
        }

        return originalFrame;
    }

    public Image<Bgr, byte> DrawActions(string action, Image<Bgr, byte> originalFrame)
    {
        var actionsDict = new Dictionary<string, Image<Bgr, byte>>
        {
            { "A", imgForward },
            { "P", imgStop },
            { "I", imgLeft },
            { "D", imgRight },
            { "R", imgReverse }
        };

        if (actionsDict.ContainsKey(action))
        {
            var movementImage = actionsDict[action];
            originalFrame = DrawImage(originalFrame, movementImage);
        }

        return originalFrame;
    }
}
