﻿using Aura.UI.UIExtensions;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Aura.UI.Controls.Navigation
{
    [PseudoClasses(":opened",":closed", ":selected")]
    public partial class SuperNavigationViewItemBase : TreeViewItem, IHeadered
    {
        private object _content = "Content";

        static SuperNavigationViewItemBase()
        {
            IsExpandedProperty.Changed.AddClassHandler<SuperNavigationViewItemBase>(
                (x,e) =>
                { 
                    if(x.IsExpanded == true)
                    {
                        var o_e = new RoutedEventArgs(OpenedEvent);
                        x.RaiseEvent(o_e);
                    }
                    else if(x.IsExpanded == false)
                    {
                        var c_e = new RoutedEventArgs(ClosedEvent);
                        x.RaiseEvent(c_e);
                    }
                });
            OpenedEvent.AddClassHandler<SuperNavigationViewItemBase>((x, e) => x.OnOpened(x, e));
            ClosedEvent.AddClassHandler<SuperNavigationViewItemBase>((x, e) => x.OnClosed(x, e));
            IsSelectedProperty.Changed.AddClassHandler<SuperNavigationViewItemBase>
                ((x, e) =>
                {
                    if (x.IsSelected)
                    {
                        x.OnSelected(x, e);
                    }
                    else if (!x.IsSelected)
                    {
                        x.OnDeselected(x, e);
                    }
                });
        }

        protected virtual void OnDeselected(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            Debug.WriteLine("I'm deselected");
        }

        protected virtual void OnSelected(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            Debug.WriteLine("I'm selected");
        }

        protected virtual void OnOpened(object sender, RoutedEventArgs e)
        {
            PseudoClasses.Remove(":closed");
            PseudoClasses.Add(":opened");
        }

        protected virtual void OnClosed(object sender, RoutedEventArgs e)
        {
            PseudoClasses.Remove(":opened");
            PseudoClasses.Add(":closed");

            /*if (this.GetParentTOfLogical<Control>() is SuperNavigationView)
            {
                return;
            }
            else if(this.GetParentTOfLogical<Control>() is SuperNavigationViewItem s)
            {
                var p = this.GetParentTOfLogical<SuperNavigationView>();

                s.IsSelected = true;
                p.SelectedItem = s;
            }*/
        }

        public event EventHandler<RoutedEventArgs> Opened
        {
            add => AddHandler(OpenedEvent, value);
            remove => RemoveHandler(OpenedEvent, value);
        }
        public static readonly RoutedEvent<RoutedEventArgs> OpenedEvent =
            RoutedEvent.Register<SuperNavigationViewItemBase,RoutedEventArgs>(nameof(Opened), RoutingStrategies.Bubble);

        public event EventHandler<RoutedEventArgs> Closed
        {
            add => AddHandler(ClosedEvent, value);
            remove => RemoveHandler(ClosedEvent, value);
        }
        public static readonly RoutedEvent<RoutedEventArgs> ClosedEvent =
            RoutedEvent.Register<SuperNavigationViewItemBase, RoutedEventArgs>(nameof(Closed), RoutingStrategies.Bubble);


        public object Content
        {
            get => _content;
            set => SetAndRaise(ContentProperty, ref _content, value);
        }
        public static readonly DirectProperty<SuperNavigationViewItemBase, object> ContentProperty =
            AvaloniaProperty.RegisterDirect<SuperNavigationViewItemBase, object>(
                nameof(Content),
                o => o.Content,
                (o,v) => o.Content = v);

        private IImage _icon;
        public IImage Icon
        {
            get => _icon;
            set => SetAndRaise(IconProperty, ref _icon, value);
        }
        public readonly static DirectProperty<SuperNavigationViewItemBase, IImage> IconProperty =
            AvaloniaProperty.RegisterDirect<SuperNavigationViewItemBase, IImage>(
                nameof(Icon),
                o => o.Icon,
                (o, v) => o.Icon = v);

        private object _title = "Title";
        public object Title
        {
            get => _title;
            set => SetAndRaise(TitleProperty, ref _title, value);
        }
        public static readonly DirectProperty<SuperNavigationViewItemBase, object> TitleProperty =
            AvaloniaProperty.RegisterDirect<SuperNavigationViewItemBase, object>(
                nameof(Title),
                o => o.Title,
                (o, v) => o.Title = v);

        public bool IsOpen
        {
            get => GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }
        public static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<SuperNavigationView, bool>(nameof(IsOpen),true);
    }
}
