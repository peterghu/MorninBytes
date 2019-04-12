using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using MorninBytes.ViewModels;
using MorninBytes.Models;
using System.Windows.Input;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using System.ComponentModel;

namespace MorninBytes.Views
{
    public partial class WebsiteManager
    {
        private Point _startPoint;
        private DragAdorner _adorner;
        private AdornerLayer _layer;
        private bool _dragIsOutOfScope = false;

        public WebsiteManager()
        {
            InitializeComponent();
        }

        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }


        private void ListViewPreviewMouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            //Point mousePos = e.GetPosition(null);
            //Vector diff = _startPoint - mousePos;
            //if (_startPoint.X == 0 || _startPoint.Y == 0)
             //   return;
            if (e.LeftButton == MouseButtonState.Pressed)
            { 
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    SetupDrag(e);
                }
            }
        }

        private void SetupDrag(MouseEventArgs e)
        {
            // Get the dragged ListViewItem
            ListView listView = this.ListWebsites; //use to be sender
            ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (listViewItem == null)
                return;

            Websites dest = (Websites)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);

            //setup the drag adorner.
            InitialiseAdorner(listViewItem);

            //add handles to update the adorner.
            listView.PreviewDragOver += ListViewDragOver;
            listView.DragLeave += ListViewDragLeave;
            listView.DragEnter += ListViewDragEnter;

            //Console.WriteLine("original source" + e.OriginalSource.ToString());
            // Initialize the drag & drop operation
            DataObject dragData = new DataObject("myFormat", dest);
            DragDropEffects derp = DragDrop.DoDragDrop(listView, dragData, DragDropEffects.Move);

            //cleanup 
            listView.PreviewDragOver -= ListViewDragOver;
            listView.DragLeave -= ListViewDragLeave;
            listView.DragEnter -= ListViewDragEnter;

            if (_adorner != null)
            {
                AdornerLayer.GetAdornerLayer(listView).Remove(_adorner);
                _adorner = null;
            }
        }

        private void InitialiseAdorner(ListViewItem listViewItem)
        {
            VisualBrush brush = new VisualBrush(listViewItem);
            _adorner = new DragAdorner((UIElement)listViewItem, listViewItem.RenderSize, brush);
            _adorner.Opacity = 0.5;
            _adorner.OffsetTop = 100;
            _adorner.OffsetLeft = 50;
            _layer = AdornerLayer.GetAdornerLayer(this.ListWebsites as Visual);
            _layer.Add(_adorner);
        }

        void ListViewQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (this._dragIsOutOfScope)
            {
                e.Action = DragAction.Cancel;
                e.Handled = true;
            }
        }

        private void ListViewPreviewMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            _startPoint = e.GetPosition(null);
        }

        private void ListViewDragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        void ListViewDragLeave(object sender, DragEventArgs e)
        {
            if (e.OriginalSource == this.ListWebsites)
            {
                Point p = e.GetPosition(this.ListWebsites);
                Rect r = VisualTreeHelper.GetContentBounds(this.ListWebsites);
                Console.WriteLine(p);
                if (!r.Contains(p))
                {
                    this._dragIsOutOfScope = true;
                    e.Handled = true;
                }
            }
        }

        void ListViewDragOver(object sender, DragEventArgs args)
        {
            if (_adorner != null)
            {
                _adorner.OffsetLeft = args.GetPosition(this.ListWebsites).X;
                _adorner.OffsetTop = args.GetPosition(this.ListWebsites).Y - _startPoint.Y;
            }
        }

        private void ListViewDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                //test
                Console.WriteLine("Pos:" + _adorner.OffsetLeft +  " " + _adorner.OffsetTop);

                ListView listView = sender as ListView;
                Websites dest = e.Data.GetData("myFormat") as Websites;
                ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                WebsiteUIViewModel dataContext = (WebsiteUIViewModel)this.DataContext;

                if (listViewItem != null)
                {
                    Websites targetDest = (Websites)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    int index = listView.Items.IndexOf(targetDest);

                    if (index >= 0)
                    {
                        dataContext.MyWebsiteList.Remove(dest);
                        dataContext.MyWebsiteList.Insert(index, dest);
                    }
                }
                else
                {
                    dataContext.MyWebsiteList.Remove(dest);
                    dataContext.MyWebsiteList.Add(dest);
                }
            }
        }

    }
}
