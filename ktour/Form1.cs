using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ktour
{
    public partial class Form1 : Form
    {
        private ChessBoard board;

        public Form1()
        {
            InitializeComponent();
            board = new ChessBoard(8, 8);
            board.BackgroundWorker = solutionBackgroundWorker;
            BuildGrid(8, 8);
            SetDoubleBuffered(ChessBoardGrid);
        }

        public static void SetDoubleBuffered(Control c)
        {
            System.Reflection.PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        private void BuildGrid(int rows, int cols)
        {
            ChessBoardGrid.Columns.Clear();
            for (int col = 0; col < cols; col++)
            {
                DataGridViewColumn dataGridViewColumn = new DataGridViewColumn { FillWeight = 1, Width = 41, CellTemplate = new DataGridViewTextBoxCell() };
                ChessBoardGrid.Columns.Add(dataGridViewColumn);
            }
            for (int row = 0; row < rows; row++)
                ChessBoardGrid.Rows.Add("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            solveButton.Enabled = false;
            boardSizeButton.Enabled = false;
            solutionBackgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = board.AttemptWarnsdorfSolution();
        }

        private void ChessBoardGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!board.PlaceKnight(new Location(e.RowIndex, e.ColumnIndex)))
                MessageBox.Show("Knight has already been placed");
            else
            {
                ChessBoardGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "K";
                statusLabel.Text = "";
                solveButton.Enabled = true;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result)
            {
                forwardButton.Enabled = true;
                MessageBox.Show("Solution found");
            }
            else
                MessageBox.Show("No solution");
            boardSizeButton.Enabled = true;
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            board.MoveSolutionForward();
            if (board.CanMoveForward())
                forwardButton.Enabled = true;
            else
                forwardButton.Enabled = false;
            if (board.CanMoveBack())
                backButton.Enabled = true;
            else
                backButton.Enabled = false;
            RefreshBoardGrid();
        }

        private void RefreshBoardGrid()
        {
            for (int row = 0; row < board.NumRows(); row++)
                for (int col = 0; col < board.NumCols(); col++)
                {
                    if (board.SquareStateAt(row, col) == ChessBoard.SquareState.Empty)
                        ChessBoardGrid.Rows[row].Cells[col].Value = "";
                    else if (board.SquareStateAt(row, col) == ChessBoard.SquareState.Occupied)
                    {
                        ChessBoardGrid.Rows[row].Cells[col].Value = "K";
                        ChessBoardGrid.CurrentCell = ChessBoardGrid.Rows[row].Cells[col];
                    }
                    else
                        ChessBoardGrid.Rows[row].Cells[col].Value = "O";
                }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            board.MoveSolutionBack();
            if (board.CanMoveForward())
                forwardButton.Enabled = true;
            else
                forwardButton.Enabled = false;
            if (board.CanMoveBack())
                backButton.Enabled = true;
            else
                backButton.Enabled = false;
            RefreshBoardGrid();
        }

        private void boardSizeButton_Click(object sender, EventArgs e)
        {
            int rows = (int)rowsUpDown.Value;
            int cols = (int)colsUpDown.Value;
            board = new ChessBoard(rows, cols);
            board.BackgroundWorker = solutionBackgroundWorker;
            BuildGrid(rows, cols);
            RefreshBoardGrid();
            solveButton.Enabled = forwardButton.Enabled = backButton.Enabled = false;
        }

        private void SolutionBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusLabel.Text = e.UserState?.ToString();
        }
    }
}
