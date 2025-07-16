using System;
using System.Collections.Generic;
using System.Linq; // Para .ToList()
using System.Drawing; // Para Bitmap si usas imágenes
using Emgu.CV; // Si usas Emgu CV
using Emgu.CV.Structure; // Para Image<Bgr, Byte>

public class GestureDetector
{
    private HandProcessing handDetector;
    private DrawingFunctions draw;

    public GestureDetector()
    {
        handDetector = new HandProcessing(thresholdDetection: 0.9);
        draw = new DrawingFunctions();
    }

    private string FingersInterpretation(List<int> fingersUp)
    {
        var commands = new Dictionary<(int, int, int, int, int), string>
        {
            { (0, 0, 0, 0, 0), "A" },
            { (1, 1, 1, 1, 1), "P" },
            { (1, 0, 0, 0, 0), "I" },
            { (0, 0, 0, 0, 1), "D" },
            { (1, 0, 0, 0, 1), "R" }
        };

        var key = (fingersUp[0], fingersUp[1], fingersUp[2], fingersUp[3], fingersUp[4]);

        if (commands.ContainsKey(key))
        {
            return commands[key];
        }
        else
        {
            return "";
        }
    }

    public (string, Image<Bgr, byte>) GestureInterpretation(Image<Bgr, byte> img)
    {
        var frame = img.Clone();
        frame = handDetector.FindHands(frame);
        var (handList, bbox) = handDetector.FindPosition(frame, drawBox: false);

        if (handList.Count == 21)
        {
            var fingersUp = handDetector.FingersUp(handList);
            var command = FingersInterpretation(fingersUp);
            frame = draw.DrawActions(command, frame);
            return (command, frame);
        }
        else
        {
            return ("P", frame);
        }
    }
}
