using System;
using ChatView.iOS.NativeCells;
using ChatView.Shared.Views;
using Foundation;
using UIKit;

namespace ChatView.iOS
{
    public class ListViewDataSourceWrapper : UITableViewSource
    {
        private readonly UITableViewSource _underlyingTableSource;

        public ListViewDataSourceWrapper(UITableViewSource underlyingTableSource)
        {
            this._underlyingTableSource = underlyingTableSource;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            return this._underlyingTableSource.GetCell(tableView, indexPath);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._underlyingTableSource.RowsInSection(tableview, section);
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return this._underlyingTableSource.GetHeightForHeader(tableView, section);
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            return this._underlyingTableSource.GetViewForHeader(tableView, section);
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return this._underlyingTableSource.NumberOfSections(tableView);
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            this._underlyingTableSource.RowSelected(tableView, indexPath);
        }

        public override string[] SectionIndexTitles(UITableView tableView)
        {
            return this._underlyingTableSource.SectionIndexTitles(tableView);
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return this._underlyingTableSource.TitleForHeader(tableView, section);
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            //TODO change ImageNativeCell to BaseNativeCell
            var lol = GetCellInternal(tableView, indexPath);
            var uiCell = (ImageNativeCell)GetCellInternal(tableView, indexPath);

            uiCell.SetNeedsLayout();
            uiCell.LayoutIfNeeded();

            return uiCell.GetHeight(tableView);
        }

        private UITableViewCell GetCellInternal(UITableView tableView, NSIndexPath indexPath)
        {
            return this._underlyingTableSource.GetCell(tableView, indexPath);
        }
    }
}
