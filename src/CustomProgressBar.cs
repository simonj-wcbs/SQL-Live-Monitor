using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace SQLMonitor
{
    [DefaultEvent("ValueChanged")]
    public class CustomProgressBar : UserControl 
    {
        // Fields
        private Rectangle r; 
        private GraphicsPath rr;
        private Rectangle lr; 
        private LinearGradientBrush lg;
        private Rectangle rrr; 
        private LinearGradientBrush rg;
        private Rectangle rrrr; 
        private Rectangle llr; 
        private LinearGradientBrush llg; 
        private ColorBlend lc;
        private Rectangle rrrrr; 
        private LinearGradientBrush rrg;
        private ColorBlend rc;
        private Rectangle tr; 
        private GraphicsPath tp;
        private LinearGradientBrush tg;
        private Rectangle br; 
        private GraphicsPath bp;
        private LinearGradientBrush bg; 
        private Rectangle gr;
        private LinearGradientBrush lgb; 
        private ColorBlend ccb;
        private Rectangle clip; 
        private Rectangle dir; 
        private GraphicsPath dirr; 
        private Rectangle dor; 
        private GraphicsPath dorr; 
        
        private int mGlowPosition = -325;
        private System.Windows.Forms.Timer mGlowAnimation = new System.Windows.Forms.Timer();
        private System.ComponentModel.IContainer components = null;

        public CustomProgressBar()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            if (!InDesignMode())
            {
                mGlowAnimation.Tick += new EventHandler(mGlowAnimation_Tick);
                mGlowAnimation.Interval = 15;
                if (Value < MaxValue) { mGlowAnimation.Start(); }
            }
        }

        protected override void Dispose(bool disposing)
        {
            rr?.Dispose();
            lg?.Dispose();
            rg?.Dispose();
            llg?.Dispose(); 
            rrg?.Dispose();
            tp?.Dispose();
            tg?.Dispose();
            bp?.Dispose();
            bg?.Dispose();
            lgb?.Dispose();                 

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Name = "CustomProgressBar";
            this.Size = new System.Drawing.Size(264, 32);
            this.Paint += new PaintEventHandler(CustomProgressBar_Paint);

            // init all variables
            r = this.ClientRectangle; r.Width--; r.Height--;
            rr = RoundRect(r, 2, 2, 2, 2);
            lr = new Rectangle(2, 2, 10, this.Height - 5);
            lg = new LinearGradientBrush(lr, Color.FromArgb(30, 0, 0, 0), Color.Transparent, LinearGradientMode.Horizontal);
            rrr = new Rectangle(this.Width - 12, 2, 10, this.Height - 5);
            rg = new LinearGradientBrush(rrr, Color.Transparent, Color.FromArgb(20, 0, 0, 0), LinearGradientMode.Horizontal);
            rrrr = new Rectangle(1, 2, this.Width - 3, this.Height - 3);
            llr = new Rectangle(1, 2, 15, this.Height - 3);
            llg = new LinearGradientBrush(llr, Color.White, Color.White, LinearGradientMode.Horizontal);
            lc = new ColorBlend(3);
            lc.Colors = new Color[] { Color.Transparent, Color.FromArgb(40, 0, 0, 0), Color.Transparent };
            lc.Positions = new float[] { 0.0F, 0.2F, 1.0F };
            llg.InterpolationColors = lc;
            rrrrr = new Rectangle(this.Width - 3, 2, 15, this.Height - 3); 
            rrrrr.X = (int)(Value * 1.0F / (MaxValue - MinValue) * this.Width) - 14;
            rrg = new LinearGradientBrush(rrrrr, Color.Black, Color.Black, LinearGradientMode.Horizontal);
            rc = new ColorBlend(3);
            rc.Colors = new Color[] { Color.Transparent, Color.FromArgb(40, 0, 0, 0), Color.Transparent };
            rc.Positions = new float[] { 0.0F, 0.8F, 1.0F };
            rrg.InterpolationColors = rc;
            tr = new Rectangle(1, 1, this.Width - 1, 6);
            tp = RoundRect(tr, 2, 2, 0, 0);
            tg = new LinearGradientBrush(tr, Color.White, Color.FromArgb(128, Color.White), LinearGradientMode.Vertical);
            br = new Rectangle(1, this.Height - 8, this.Width - 1, 6);
            bp = RoundRect(br, 0, 0, 2, 2);
            bg = new LinearGradientBrush(br, Color.Transparent, Color.FromArgb(100, this.HighlightColor), LinearGradientMode.Vertical);
            gr = new Rectangle(mGlowPosition, 0, 60, this.Height);
            lgb = new LinearGradientBrush(gr, Color.White, Color.White, LinearGradientMode.Horizontal);
            ccb = new ColorBlend(4);
            ccb.Colors = new Color[] { Color.Transparent, this.GlowColor, this.GlowColor, Color.Transparent };
            ccb.Positions = new float[] { 0.0F, 0.5F, 0.6F, 1.0F };
            lgb.InterpolationColors = ccb;
            clip = new Rectangle(1, 2, this.Width - 3, this.Height - 3);
            dir = this.ClientRectangle;
            dir.X++; dir.Y++; dir.Width -= 3; dir.Height -= 3;
            dor = this.ClientRectangle; dor.Width--; dor.Height--;
        }

        // Properties
        private int mValue = 0;
        [Category("Value"), DefaultValue(0), Description("The value that is displayed on the progress bar.")]
        public int Value
        {
            get { return mValue; }
            set 
            { 
                if (value > MaxValue || value < MinValue) { return; }
                mValue = value;
                if (value < MaxValue) { mGlowAnimation.Start(); }
                if (value == MaxValue) { mGlowAnimation.Stop(); }
                ValueChanged?.Invoke(this, new System.EventArgs());
                this.Invalidate(); 
            }
        }

        private Color mNewColor = Color.Green;
        [Category("Value"), Description("The Color that the bar will be draw in")]
        public Color NewColor
        {
            get { return mNewColor; }
            set
            {
                mNewColor = value;
                this.Invalidate();
            }
        }
        
        private int mMaxValue = 100;
        [Category("Value"), DefaultValue(100), Description("The maximum value for the Value property.")]
        public int MaxValue
        {
            get { return mMaxValue; }
            set 
            { 
                mMaxValue = value; 
                if (Value > MaxValue) { Value = MaxValue; }
                if (Value < MaxValue) { mGlowAnimation.Start(); } 
                MaxChanged?.Invoke(this, new System.EventArgs());
                this.Invalidate(); 
            }
        }

        private int mMinValue = 0;
        [Category("Value"), DefaultValue(0), Description("The minimum value for the Value property.")]
        public int MinValue
        {
            get { return mMinValue; }
            set 
            {
                mMinValue = value; 
                if (Value < MinValue) { Value = MinValue; }
                MinChanged?.Invoke(this, new System.EventArgs());
                this.Invalidate(); 
            }
        }

        private Color mStartColor = Color.FromArgb(210, 0, 0);
        [Category("Bar"), DefaultValue(typeof(Color), "210, 0, 0")]
        public Color StartColor
        {
            get { return mStartColor; }
            set { mStartColor = value; this.Invalidate(); }
        }

        private Color mEndColor = Color.FromArgb(0, 211, 40);
        [Category("Bar"), DefaultValue(typeof(Color), "0, 211, 40")]
        public Color EndColor
        {
            get { return mEndColor; }
            set { mEndColor = value; this.Invalidate(); }
        }

        private Color mHighlightColor = Color.White;
        [Category("Highlights and Glows"), DefaultValue(typeof(Color), "White")]
        public Color HighlightColor
        {
            get { return mHighlightColor; }
            set { mHighlightColor = value; this.Invalidate(); }
        }

        private Color mBackgroundColor = Color.FromArgb(201, 201, 201);
        [Category("Highlights and Glows"), DefaultValue(typeof(Color), "201,201,201")]
        public Color BackgroundColor
        {
            get { return mBackgroundColor; }
            set { mBackgroundColor = value; this.Invalidate(); }
        }

        private bool mAnimate = true;
        [Category("Highlights and Glows"), DefaultValue(true)]
        public bool Animate
        {
            get { return mAnimate; }
            set
            {
                mAnimate = value; 
                if (value) { mGlowAnimation.Start(); } else { mGlowAnimation.Stop(); }
                this.Invalidate(); 
            }
        }

        private Color mGlowColor = Color.FromArgb(150, 255, 255, 255);
        [Category("Highlights and Glows"), DefaultValue(typeof(Color), "150, 255, 255, 255")]
        public Color GlowColor
        {
            get { return mGlowColor; }
            set { mGlowColor = value; this.Invalidate(); }
        }

        // Drawing Methods
        private void DrawBackground(Graphics g) => g.FillPath(new SolidBrush(this.BackgroundColor), rr);

        private void DrawBackgroundShadows(Graphics g)
        {
            lr.X--;
            g.FillRectangle(lg, lr);				
            g.FillRectangle(rg, rrr);
        }

        private void DrawBar(Graphics g)
        {				 
            rrrr.Width = (int)(Value * 1.0F / (MaxValue - MinValue) * this.Width);
            g.FillRectangle(new SolidBrush(this.NewColor), rrrr);
        }

        private void DrawBarShadows(Graphics g)
        {
            llr.X--;
            g.FillRectangle(lg, llr);
            g.FillRectangle(rrg, rrrrr);
        }

        private void DrawHighlight(Graphics g)
        {
            g.SetClip(tp);
            g.FillPath(tg, tp);
            g.ResetClip();
        
            g.SetClip(bp);
            g.FillPath(bg, bp);
            g.ResetClip();
        }

        private void DrawInnerStroke(Graphics g)
        {
            Rectangle r = this.ClientRectangle; 
            r.X++; r.Y++; r.Width -= 3; r.Height -= 3;
            GraphicsPath rr = RoundRect(r, 2, 2, 2, 2);
            g.DrawPath(new Pen(Color.FromArgb(100, Color.White)), rr);
        }

        private void DrawGlow(Graphics g)
        {
            clip.Width = (int)(Value * 1.0F / (MaxValue - MinValue) * this.Width);
            g.SetClip(clip);
            g.FillRectangle(lgb, r);
            g.ResetClip();
        }

        private void DrawOuterStroke(Graphics g)
        {
            Rectangle r = this.ClientRectangle; r.Width--; r.Height--;
            GraphicsPath rr = RoundRect(r, 2, 2, 2, 2);
            g.DrawPath(new Pen(Color.FromArgb(178, 178, 178)), rr);
        }

        // Helper Methods
        private GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
        {
            float x = r.X, y = r.Y, w = r.Width, h = r.Height;
            GraphicsPath rr = new GraphicsPath();
            rr.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
            rr.AddLine(x + r1, y, x + w - r2, y);
            rr.AddBezier(x + w - r2, y, x + w, y, x + w, y + r2, x + w, y + r2);
            rr.AddLine(x + w, y + r2, x + w, y + h - r3);
            rr.AddBezier(x + w, y + h - r3, x + w, y + h, x + w - r3, y + h, x + w - r3, y + h);
            rr.AddLine(x + w - r3, y + h, x + r4, y + h);
            rr.AddBezier(x + r4, y + h, x, y + h, x, y + h - r4, x, y + h - r4);
            rr.AddLine(x, y + h - r4, x, y + r1);
            return rr;
        }

        private bool InDesignMode() => (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

        private Color GetIntermediateColor()
        {
            Color c = this.StartColor;
            Color c2 = this.EndColor;

            float pc = this.Value * 1.0F / (this.MaxValue - this.MinValue);

            int ca = c.A, cr = c.R, cg = c.G, cb = c.B;
            int c2a = c2.A, c2r = c2.R, c2g = c2.G, c2b = c2.B;
            
            int a = (int)Math.Abs(ca + (ca - c2a) * pc);
            int r = (int)Math.Abs(cr - ((cr - c2r) * pc));
            int g = (int)Math.Abs(cg - ((cg - c2g) * pc));
            int b = (int)Math.Abs(cb - ((cb - c2b) * pc));

            a = Math.Min(a, 255);
            r = Math.Min(r, 255);
            g = Math.Min(g, 255);
            b = Math.Min(b, 255);

            return Color.FromArgb(a, r, g, b);
        }

        // Event Handlers
        private void CustomProgressBar_Paint(object sender, PaintEventArgs e)
        {
            DrawBackground(e.Graphics);
            DrawBackgroundShadows(e.Graphics);
            DrawBar(e.Graphics);
            DrawBarShadows(e.Graphics);
            DrawHighlight(e.Graphics);
            DrawInnerStroke(e.Graphics);
            DrawGlow(e.Graphics);
            DrawOuterStroke(e.Graphics);
        }

        private void mGlowAnimation_Tick(object sender, EventArgs e)
        {
            if (Animate)
            {
                mGlowPosition += 2;
                if (mGlowPosition > this.Width)
                {
                    mGlowPosition = -325;
                }
                this.Invalidate();
            }
            else
            {
                mGlowAnimation.Stop();
            }
        }

        // Events
        public event EventHandler ValueChanged;
        public event EventHandler MinChanged;
        public event EventHandler MaxChanged;
    }
}
