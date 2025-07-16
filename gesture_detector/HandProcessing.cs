using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;


public class HandProcessing
{
    private bool mode;
    private int maxHands;
    private int complexity;
    private double confDetection;
    private double confTracking;

    private dynamic? mpHands; // Aquí deberías reemplazar con tu implementación de detector de manos en C#
    private dynamic? hands;
    private dynamic? draw;

    private List<int> tip = new List<int> { 4, 8, 12, 16, 20 };

    public HandProcessing(bool mode = false, int hands = 1, int modelComplexity = 0, double thresholdDetection = 0.5, double thresholdTracking = 0.5)
    {
        this.mode = mode;
        this.maxHands = hands;
        this.complexity = modelComplexity;
        this.confDetection = thresholdDetection;
        this.confTracking = thresholdTracking;

        // Sustituye estas líneas por tu detector de manos en C# (por ejemplo, Mediapipe wrapper)
        this.mpHands = null; 
        this.hands = null;
        this.draw = null;
    }

    public Image<Bgr, byte> FindHands(Image<Bgr, byte> frame, bool drawLandmarks = true)
    {
        // Convertir BGR a RGB si es necesario para tu modelo
        // Aquí es solo un placeholder
        var imgColor = frame.Clone(); // En Emgu CV ya es BGR; convierte si tu modelo necesita RGB

        // Procesar frame con tu detector de manos
        // this.results = this.hands.Process(imgColor);
        // Sustituye esto por tu implementación real

        // if (this.results.multi_hand_landmarks != null)
        // {
        //     foreach (var mano in this.results.multi_hand_landmarks)
        //     {
        //         if (drawLandmarks)
        //         {
        //             this.draw.draw_landmarks(frame, mano, this.mpHands.HAND_CONNECTIONS);
        //         }
        //     }
        // }

        return frame;
    }

    public (List<List<int>>, Rectangle) FindPosition(Image<Bgr, byte> frame, int hand = 0, bool drawPoints = true, bool drawBox = true, Bgr? color = null)
    {
        List<int> xlist = new List<int>();
        List<int> ylist = new List<int>();
        List<List<int>> handsList = new List<List<int>>();
        Rectangle bbox = Rectangle.Empty;

        // if (this.results.multi_hand_landmarks != null)
        // {
        //     var myHand = this.results.multi_hand_landmarks[hand];
        //     foreach (var (id, lm) in myHand.landmark.Select((lm, id) => (id, lm)))
        //     {
        //         int alto = frame.Height;
        //         int ancho = frame.Width;
        //         int cx = (int)(lm.x * ancho);
        //         int cy = (int)(lm.y * alto);
        //         xlist.Add(cx);
        //         ylist.Add(cy);
        //         handsList.Add(new List<int> { id, cx, cy });

        //         if (drawPoints)
        //         {
        //             CvInvoke.Circle(frame, new Point(cx, cy), 3, new MCvScalar(0, 0, 0), -1);
        //         }
        //     }

        //     int xmin = xlist.Min();
        //     int xmax = xlist.Max();
        //     int ymin = ylist.Min();
        //     int ymax = ylist.Max();

        //     bbox = new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);

        //     if (drawBox)
        //     {
        //         var boxColor = color ?? new Bgr(0, 255, 0);
        //         CvInvoke.Rectangle(frame, new Rectangle(xmin - 20, ymin - 20, (xmax - xmin) + 40, (ymax - ymin) + 40), boxColor.MCvScalar, 2);
        //     }
        // }

        return (handsList, bbox);
    }

    public List<int> FingersUp(List<List<int>> keypointsList)
    {
        List<int> fingers = new List<int>();

        if (keypointsList[tip[0]][1] > keypointsList[tip[0] - 1][1])
            fingers.Add(1);
        else
            fingers.Add(0);

        for (int i = 1; i < 5; i++)
        {
            if (keypointsList[tip[i]][2] < keypointsList[tip[i] - 2][2])
                fingers.Add(1);
            else
                fingers.Add(0);
        }

        return fingers;
    }

    public (double, Image<Bgr, byte>, List<int>) Distance(int p1, int p2, Image<Bgr, byte> frame, bool draw = true, int radius = 15, int thickness = 3)
    {
        int x1 = this.list[p1][1];
        int y1 = this.list[p1][2];
        int x2 = this.list[p2][1];
        int y2 = this.list[p2][2];

        int cx = (x1 + x2) / 2;
        int cy = (y1 + y2) / 2;

        if (draw)
        {
            CvInvoke.Line(frame, new Point(x1, y1), new Point(x2, y2), new MCvScalar(0, 0, 255), thickness);
            CvInvoke.Circle(frame, new Point(x1, y1), radius, new MCvScalar(0, 0, 255), -1);
            CvInvoke.Circle(frame, new Point(x2, y2), radius, new MCvScalar(0, 0, 255), -1);
            CvInvoke.Circle(frame, new Point(cx, cy), radius, new MCvScalar(0, 0, 255), -1);
        }

        double length = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

        return (length, frame, new List<int> { x1, y1, x2, y2, cx, cy });
    }

    // Propiedad auxiliar para guardar puntos
    public List<List<int>> list { get; set; } = new List<List<int>>();
}
