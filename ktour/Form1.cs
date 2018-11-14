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
            for (int i = 0; i < 8; i++)
                ChessBoardGrid.Rows.Add("", "", "", "", "", "", "", "");
            board = new ChessBoard();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            solveButton.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = board.attemptSolutionWarnsdorf();
        }

        private void ChessBoardGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!board.PlaceKnight(e.RowIndex, e.ColumnIndex))
                MessageBox.Show("Knight has already been placed");
            else
            {
                ChessBoardGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "K";
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
                        ChessBoardGrid.Rows[row].Cells[col].Value = "K";
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
    }
}
