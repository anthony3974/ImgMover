using BaseClassLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Value for getting the image properties
/// </summary>
public enum Value
{
    /// <summary>
    /// Height
    /// </summary>
    Height,
    /// <summary>
    /// Width
    /// </summary>
    Width,
    /// <summary>
    /// X
    /// </summary>
    X,
    /// <summary>
    /// Y
    /// </summary>
    Y,
}
/// <summary>
/// Value for moving the picture
/// </summary>
public enum Way
{
    /// <summary>
    /// Up
    /// </summary>
    Up,
    /// <summary>
    /// Down
    /// </summary>
    Down,
    /// <summary>
    /// Left
    /// </summary>
    Left,
    /// <summary>
    /// Right
    /// </summary>
    Right,
}
/// <summary>
/// Class for managing Windows Forms Images for games (mostly)
/// </summary>
public class ImgMover : BaseClass
{
    #region class info
    /// <summary>
    /// Gets version of the class
    /// </summary>
    /// <returns>Version</returns>
    public string getVersion() { return "1"; }
    #endregion
    #region Constructors
    /// <summary>
    /// Makes an ImgMover class for image
    /// </summary>
    /// <param name="path">Sets the path for the image file</param>
    /// <param name="x">Sets the starting x value</param>
    /// <param name="y">Sets the starting y value</param>
    /// <param name="width">Sets the width value for the image</param>
    /// <param name="height">Sets the height value for the image</param>
    /// <param name="widthPer">Set width based on the default image value times widthPer (percentage) value</param>
    /// <param name="heightPer">Set height based on the default image value times heightPer (percentage) value</param>
    public ImgMover(string path, int x = 0, int y = 0, int width = 0, int height = 0, float widthPer = 0, float heightPer = 0)
    {
        MakeImage(path, x, y, width, height, widthPer, heightPer);
    }
    /// <summary>
    /// Makes an ImgMover class for rectangle
    /// </summary>
    /// <param name="recColor">Sets the color for the rectangle</param>
    /// <param name="values">Sets the size for the rectangle</param>
    /// <param name="amount">Sets the pen size for the rectangle</param>
    /// <param name="fill">Sets if rectangle should be solid</param>
    public ImgMover(Color recColor, RectangleF values, int amount = 0, bool fill = false)
    {
        MakeImage(recColor, values, amount, fill);
    }
    /// <summary>
    /// Makes an ImgMover class for poly
    /// </summary>
    /// <param name="polyColor">Sets the color for the poly</param>
    /// <param name="values">Sets the location for the poly points</param>
    /// <param name="iSize">Sets the boundaries for the poly</param>
    /// <param name="amount">Sets the pen size for the poly</param>
    /// <param name="fill">Sets if poly should be solid</param>
    public ImgMover(Color polyColor, PointF[] values, RectangleF iSize, int amount = 0, bool fill = false)
    {
        MakeImage(polyColor, values, iSize, amount, fill);
    }
    /// <summary>
    /// Makes an ImgMover class for string
    /// </summary>
    /// <param name="textColor">Sets the color for the text</param>
    /// <param name="text">Sets the text to display</param>
    /// <param name="font">Sets the font for the text</param>
    /// <param name="iSize">Sets the boundaries for the text</param>
    public ImgMover(Color textColor, string text, Font font, RectangleF iSize)
    {
        MakeImage(textColor, text, font, iSize);
    }
    #endregion
    #region vars
    private enum Type { Image, Rec, Poly, Word }
    private Type type = Type.Image;
    private Image imgMain, imgUp;
    private float imgX, imgY, imgWidth, imgHeight;
    private PointF[] poly;
    private string word;
    private Font font;
	private int angle;
    #endregion
    #region methods
    /// <summary>
    /// Makes an ImgMover class for image
    /// </summary>
    /// <param name="path">Sets the path for the image file</param>
    /// <param name="x">Sets the starting x value</param>
    /// <param name="y">Sets the starting y value</param>
    /// <param name="width">Sets the width value for the image</param>
    /// <param name="height">Sets the height value for the image</param>
    /// <param name="widthPer">Set width based on the default image value times widthPer (percentage) value</param>
    /// <param name="heightPer">Set height based on the default image value times heightPer (percentage) value</param>
    public void MakeImage(string path, int x = 0, int y = 0, int width = 0, int height = 0, float widthPer = 0, float heightPer = 0)
    {
        type = Type.Image; // set the type for the change color option
        Delete(); // delete the old resouce 
        imgMain = Image.FromFile(path); // set image with the path
        imgUp = imgMain; // change the up image for rotation
        // if the width and height is 0 then set it
        if (width == 0) width = imgMain.Width;
        if (height == 0) height = imgMain.Height;
        // if the PER width and PER height is 0 then set it
        if (widthPer != 0) width = (int)(imgMain.Width * widthPer);
        if (heightPer != 0) height = (int)(imgMain.Height * heightPer);
        // set image values
        imgX = x;
        imgY = y;
        imgWidth = width;
        imgHeight = height;
    }
    /// <summary>
    /// Makes an ImgMover class for rectangle
    /// </summary>
    /// <param name="recColor">Sets the color for the rectangle</param>
    /// <param name="values">Sets the size for the rectangle</param>
    /// <param name="amount">Sets the pen size for the rectangle</param>
    /// <param name="fill">Sets if rectangle should be solid</param>
    public void MakeImage(Color recColor, RectangleF values, int amount = 0, bool fill = false) // make rectangle Fun
    {
        type = Type.Rec; // set the type for the change color option
        Delete(); // delete the old resouce
        // create a new empty bitmap to hold base image
        Bitmap returnBitmap = new Bitmap(Convert.ToInt32(values.Width + (amount * 2)), Convert.ToInt32(values.Height + (amount * 2)));
        // make a graphics object from the empty bitmap
        Graphics c = Graphics.FromImage(returnBitmap);
        // draw passed rectangle into graphics object
        if (fill == false) c.DrawRectangle(new Pen(recColor, amount), 0 + amount, 0 + amount, values.Width, values.Height);
        if (fill == true) c.FillRectangle(new SolidBrush(recColor), 0 + amount, 0 + amount, values.Width, values.Height);
        c.Dispose(); // dispose of unneeded resouses
        imgMain = returnBitmap; // make a main image
        imgUp = imgMain; // change the up image for rotation
        // make Image values
        imgX = values.X;
        imgY = values.Y;
        imgWidth = imgMain.Width;
        imgHeight = imgMain.Height;
    }
    /// <summary>
    /// Makes an ImgMover class for poly
    /// </summary>
    /// <param name="polyColor">Sets the color for the poly</param>
    /// <param name="values">Sets the location for the poly points</param>
    /// <param name="iSize">Sets the boundaries for the poly</param>
    /// <param name="amount">Sets the pen size for the poly</param>
    /// <param name="fill">Sets if poly should be solid</param>
    public void MakeImage(Color polyColor, PointF[] values, RectangleF iSize, int amount = 0, bool fill = false) // make poly Fun
    {
        type = Type.Poly; // set the type for the change color option
        Delete(); // delete the old resouce 
        // set x, y, width and height
        float x = iSize.X, y = iSize.Y, width = iSize.Width, height = iSize.Height;
        // create a new empty bitmap to hold base image
        Bitmap returnBitmap = new Bitmap(Convert.ToInt32(width + (amount * 2)), Convert.ToInt32(height + (amount * 2)));
        // make a graphics object from the empty bitmap
        Graphics c = Graphics.FromImage(returnBitmap);
        // draw passed poly into graphics object
        if (fill == false) c.DrawPolygon(new Pen(polyColor, amount), values);
        if (fill == true) c.FillPolygon(new SolidBrush(polyColor), values);
        c.Dispose(); // dispose of unneeded resouses
        imgMain = returnBitmap; // make a main image
        imgUp = imgMain; // change the up image for rotation
        // make Image values
        imgX = x;
        imgY = y;
        imgWidth = width;
        imgHeight = height;
        poly = values; // make a poly Ray
    }
    /// <summary>
    /// Makes an ImgMover class for string
    /// </summary>
    /// <param name="textColor">Sets the color for the text</param>
    /// <param name="text">Sets the text to display</param>
    /// <param name="font">Sets the font for the text</param>
    /// <param name="iSize">Sets the boundaries for the text</param>
    public void MakeImage(Color textColor, string text, Font font, RectangleF iSize) // make string Fun
    {

        type = Type.Word; // set the type for the change color option
        word = text; // set the word from text
        this.font = font; // set the font
        Delete(); // delete the old resouce
        // create a new empty bitmap to hold base image
        Bitmap returnBitmap = new Bitmap(Convert.ToInt32(iSize.Width), Convert.ToInt32(iSize.Height));
        // make a graphics object from the empty bitmap
        Graphics c = Graphics.FromImage(returnBitmap);
        // draw passed string into graphics object
        c.DrawString(text, font, new SolidBrush(textColor), 0, 0);
        c.Dispose(); // dispose of unneeded resouses
        imgMain = returnBitmap; // make a main image
        imgUp = imgMain; // change the up image for rotation
        // make Image values
        imgX = iSize.X;
        imgY = iSize.Y;
        imgWidth = imgMain.Width;
        imgHeight = imgMain.Height;
    }
    /// <summary>
    /// Moves the picture
    /// </summary>
    /// <param name="amount">Sets the amount of pixels to move</param>
    /// <param name="direction">Sets the direction to move the picture</param>
    public void MovePic(float amount, Way direction = Way.Down)
    {
        // move the pic
        if (direction == Way.Up) imgY -= amount;
        else if (direction == Way.Right) imgX += amount;
        else if (direction == Way.Left) imgX -= amount;
        else imgY += amount;
    }
    /// <summary>
    /// Changes color of the image (dose not work on image type)
    /// </summary>
    /// <param name="color">Sets the color to change</param>
    public void Changecolor(Color color)
    {
        if (type == Type.Image) MessageBox.Show("invaild option");
        else if (type == Type.Rec) MakeImage(color, new RectangleF(imgX, imgY, imgWidth, imgHeight), fill: true);
        else if (type == Type.Poly) MakeImage(color, poly, new RectangleF(imgX, imgY, imgWidth, imgHeight), fill: true);
        else if (type == Type.Word) MakeImage(color, word, font, new RectangleF(imgX, imgY, imgWidth, imgHeight));
    }
    /// <summary>
    /// Will rotate the image
    /// </summary>
    /// <param name="angle">Sets the angle to rotate (from 0)</param>
    public void RotatePic(int angle)
    {
        // set the base angle
        this.angle = angle;
        // make a base image
        Image b = new Bitmap(imgUp);
        // create a new empty bitmap to hold the rotated image
        Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
        // make a graphics object from the empty bitmap
        Graphics c = Graphics.FromImage(returnBitmap);
        // move rotation point to center of image
        c.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
        // rotate the image
        c.RotateTransform(angle);
        // move image back
        c.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
        // draw passed image onto graphics object
        c.DrawImage(b, new Point(0, 0));
        // dispose of unneeded resouses
        b.Dispose();
        c.Dispose();
        // save the new image
        imgMain = returnBitmap;
    }
    /// <summary>
    /// Can get the values of the image
    /// </summary>
    /// <param name="Name">Returns this value</param>
    /// <returns>The value of the image with the selected value</returns>
    public float GetValue(Value Name)
    {
        if (Name == Value.Height) return imgHeight;
        else if (Name == Value.Width) return imgWidth;
        else if (Name == Value.X) return imgX;
        else if (Name == Value.Y) return imgY;
        else return -1;
    }
    /// <summary>
    /// Removes the old image to free up resoures
    /// </summary>
    public void Delete()
    {
        if (imgMain != null)
        {
            imgMain.Dispose();
            imgUp.Dispose();
        }
    }
    #endregion
    #region properties
    /// <summary>
    /// Gets/Sets image
    /// </summary>
    public Image Image
    {
        get { return imgMain; }
        set { imgMain = value; imgUp = imgMain; }
    }
    /// <summary>
    /// Gets/Sets x
    /// </summary>
    public float X
    {
        get { return imgX; }
        set { imgX = value; }
    }
    /// <summary>
    /// Gets/Sets y
    /// </summary>
    public float Y
    {
        get { return imgY; }
        set { imgY = value; }
    }
    /// <summary>
    /// Gets/Sets width
    /// </summary>
    public float Width
    {
        get { return imgWidth; }
        set { imgWidth = value; }
    }
    /// <summary>
    /// Gets/Sets height
    /// </summary>
    public float Height
    {
        get { return imgHeight; }
        set { imgHeight = value; }
    }
    /// <summary>
    /// Gets/Sets poly
    /// </summary>
    public PointF[] Poly
    {
        get { return poly; }
        set { poly = value; }
    }
    /// <summary>
    /// Gets an "empty" ImgMover object
    /// </summary>
    public static ImgMover Empty
    {
        get { return new ImgMover(Color.Transparent, new Rectangle(0, 0, 1, 1)); }
    }
    /// <summary>
    /// Gets the text
    /// </summary>
    public string Text
    {
        get { if (word != null) return word; else return ""; }
    }
    /// <summary>
    /// Gets the angle
    /// </summary>
    public int Angle
    {
        get { return angle; }
    }
    #endregion
}

