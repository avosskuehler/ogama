using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using OgamaDao.Model.Rta;
namespace Ogama.Modules.Rta.RtaReplay
{
    public class RtaPanel : IDrawControllerListener
    {

        private Control parent;
        private Panel basePanel;
        private Panel categoryPanel;
        private ComboBox categoryCombobox;
        private Panel drawingPanel;
        private Graphics g = null;

        private DrawController controller;
        private Tools tools;

        private Marker marker1 = null;
        private Marker marker2 = null;
        private Marker progressMarker = null;
        private Label labelName;
        private RtaModel model;
        private RtaCategory rtaCategory;
        private List<RtaPanel> siblings = new List<RtaPanel>();
        private double currentPlayerPositionString = 0;

        public void addRtaEvent(RtaEvent rtaEvent)
        {
            this.controller.addRtaEvent(rtaEvent);
        }

        public bool belongsToRtaEvent(RtaEvent rtaEvent)
        {
            string myCategoryName = rtaCategory.name;
            string itsCategoryName = rtaEvent.fkRtaCategory.name;
            
            log("compare " + myCategoryName + "," + itsCategoryName);

            if (myCategoryName.Equals(itsCategoryName))
            {
                return true;
            }
            if (this.rtaCategory.Equals(rtaEvent.fkRtaCategory))
            {
                return true;
            }
            return false;
        }

        public void setRtaModel(RtaModel model)
        {
            this.model = model;
            this.controller.setRtaModel(model);
        }

        public void setRtaCategory(RtaCategory rtaCategory)
        {
            this.rtaCategory = rtaCategory;
            this.controller.setRtaCategory(this.rtaCategory);
        }

        public void AddSibling(RtaPanel rtaPanel)
        {
            this.siblings.Add(rtaPanel);
        }

        public void SetName(string s)
        {
            this.labelName.Text = s;
        }

        public RtaPanel(string name)
        {
            init();
            SetName(name);
        }

        public RtaPanel()
        {
            init();
        }

        private void init()
        {
            this.controller = new DrawController();
            this.controller.register(this);

            this.tools = new Tools();

            this.InitializeComponent();
        }

        
        public void AddToParent(Control control)
        {
            this.parent = control;
            control.Controls.Add(this.basePanel);
        }

        public void RemoveFromParent()
        {
            this.parent.Controls.Remove(this.basePanel);
        }

        public void setLocation(int x, int y)
        {
            this.basePanel.Location = new Point(x, y);
        }

        private void InitializeComponent()
        {
            this.basePanel = createBasePanel();
            this.basePanel.Location = new Point(10, 10);

            createMarker1();

            createMarker2();

            createProgressMarker();
        }

        private void createProgressMarker()
        {
            this.progressMarker = new Marker();
            this.progressMarker.positionX = 0;
            this.progressMarker.maxXvalue = getGraphicsWidth();
            this.progressMarker.brush = Brushes.Red;
            this.progressMarker.isProgressMarker = true;
            this.progressMarker.setZlevel(-1);
            this.controller.add(this.progressMarker);
            this.controller.progressMarker = progressMarker;
        }

        private void createMarker2()
        {
            this.marker2 = new Marker();
            this.marker2.positionX = getGraphicsWidth() - marker2.width;
            this.marker2.maxXvalue = getGraphicsWidth();
            this.controller.add(this.marker2);
            this.controller.marker2 = marker2;
        }

        private void createMarker1()
        {
            this.marker1 = new Marker();
            this.marker1.positionX = 0;
            this.marker1.leftMarker = true;
            this.marker1.maxXvalue = getGraphicsWidth();
            this.controller.add(this.marker1);
            this.controller.marker1 = marker1;
        }

        private Panel createBasePanel()
        {
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.Fixed3D;
            panel.Name = "basePanel";
            panel.Size = new Size(1350, 60);
            panel.TabIndex = 0;
            
            this.categoryPanel = this.createCategoryPanel();
            panel.Controls.Add(this.categoryPanel);
            
            this.drawingPanel = this.createDrawingPanel();
            panel.Controls.Add(this.drawingPanel);

            return panel;
        }

        
        private Panel createCategoryPanel()
        {
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.None;
            panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            panel.Location = new Point(2, 2);
            panel.Size = new Size(200, 15);

            /*this.categoryCombobox = new ComboBox();
            this.categoryCombobox.Location = new Point(3, 22);
            this.categoryCombobox.Name = "categoryCombobox";
            this.categoryCombobox.Size = new Size(225, 21);
            panel.Controls.Add(this.categoryCombobox);*/

            labelName = new Label();
            labelName.Text = "";
            labelName.Size = new Size(180, 12);
            labelName.Location = new Point(2, 2);
            panel.Controls.Add(labelName);

            return panel;
        }

        private Panel createDrawingPanel()
        {
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = Color.SlateGray;
            panel.Location = new Point(10, 20);
            panel.Size = new Size(1200, 30);
            panel.Name = "drawingPanel";
            panel.Paint += new PaintEventHandler(this.onDrawingPanelPaint);

            panel.MouseMove += new MouseEventHandler(onMouseMove);
            panel.MouseDown += new MouseEventHandler(onMouseDown);
            panel.MouseUp += new MouseEventHandler(onMouseUp);
            return panel;
        }


        private void onDrawingPanelPaint(object sender, PaintEventArgs e)
        {
            this.controller.drawFigures(getGraphics());
        }

        private void onMouseMove(object sender, EventArgs e)
        {
            if (e is System.Windows.Forms.MouseEventArgs)
            {
                System.Windows.Forms.MouseEventArgs mouseEvent = (System.Windows.Forms.MouseEventArgs)e;
                Graphics g = getGraphics();
                if (mouseEvent.Button == MouseButtons.Left)
                {
                    this.controller.onMouseMove(mouseEvent.X, mouseEvent.Y, g, getGraphicsWidth());
                }
            }
        }

        private void onMouseDown(object sender, EventArgs e)
        {
            if (e is System.Windows.Forms.MouseEventArgs)
            {
                System.Windows.Forms.MouseEventArgs mouseEvent = (System.Windows.Forms.MouseEventArgs)e;
                this.controller.onMouseDown(mouseEvent.X);
            }
        }


        private void onMouseUp(object sender, EventArgs e)
        {
            if (e is System.Windows.Forms.MouseEventArgs)
            {
                System.Windows.Forms.MouseEventArgs mouseEvent = (System.Windows.Forms.MouseEventArgs)e;
                this.controller.onMouseUp(mouseEvent.X);
            }
        }





        private Graphics getGraphics()
        {
            if (this.g == null)
            {
                g = this.drawingPanel.CreateGraphics();
            }

            return g;
        }

        private int getGraphicsWidth()
        {
            return this.drawingPanel.Width;
        }

        private void log(string s)
        {
            Console.WriteLine("RtaPanel.log:" + s);
        }

        public void adjustProgressTrackerPosition(double xPositionInPercent)
        {
            this.controller.setProgressMarkerPositionInPercent(xPositionInPercent, getGraphics());
        }

        public void onProgressTrackerPositionChanged(double xPositionInPercent)
        {
            for (int i = 0; i < siblings.Count(); i++)
            {
                RtaPanel s = siblings.ElementAt(i);
                s.adjustProgressTrackerPosition(xPositionInPercent);
            }

        }

        public void onLeftMarkerPositionChanged(double xPositionInPercent)
        {
            
        }

        public void onRightMarkerPositionChanged(double xPositionInPercent)
        {
            
        }

        
        public void setCurrentPlayerPositionString(double currentPlayerPositionString)
        {
            this.currentPlayerPositionString = currentPlayerPositionString;
        }

        public double getCurrentPlayerPosition()
        {
            return this.currentPlayerPositionString;
        }


        public string getTimeValueByXposition(double xPosition)
        {
            throw new NotImplementedException();
        }
    }
}
