using System;
using System.Windows;
using System.Windows.Controls;
using FindPathProc.Lib;

namespace FindPathProc.WPFApp
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage
    {
        /// <summary>
        /// Weight matrix of graph
        /// </summary>
        private int[,] _weightMatrix;

        /// <summary>
        /// Distance matrix of graph
        /// </summary>
        private int[,] _distMatrix;

        /// <summary>
        /// Matrix with UIs, created to display weight matrix
        /// </summary>
        private UIElement[,] _uiWMatrix;

        /// <summary>
        /// Matrix with UIs, created to display distance matrix
        /// </summary>
        private UIElement[,] _uiDistMatrix;

        /// <summary>
        /// Id of start vertex
        /// </summary>
        private int _startId;

        /// <summary>
        /// Id of destination vertex
        /// </summary>
        private int _destId;

        /// <summary>
        /// Number of vertices in graph
        /// </summary>
        private int _vertsCount;

        /// <summary>
        /// Max count of vertices
        /// </summary>
        private static int maxCount = 12;

        /// <summary>
        /// Constructor, Initializes UI 
        /// </summary>
        public StartPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize matrixes with defined vertices number
        /// </summary>
        /// <param name="vertsCount">number of vertices in graph</param>
        private void InitMatrix(int vertsCount)
        {
            _weightMatrix = new int[vertsCount, vertsCount];
            _distMatrix = new int[vertsCount, vertsCount];
            _uiDistMatrix = new UIElement[vertsCount, vertsCount];
            _uiWMatrix = new UIElement[vertsCount, vertsCount];
            _vertsCount = vertsCount;
        }

        /// <summary>
        /// Initialize UI part of weight matrix for 1 el
        /// </summary>
        /// <param name="i">row Id</param>
        /// <param name="j">column Id</param>
        private void InitWUi(int i, int j)
        {
            if (j != i)
            {
                TextBox textBox = new TextBox
                {
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 20
                };
                textBox.TextChanged += WMatrix_Changed;
                textBox.SetValue(Grid.RowProperty, i);
                textBox.SetValue(Grid.ColumnProperty, j);
                textBox.VerticalContentAlignment = VerticalAlignment.Center;

                _uiWMatrix[i, j] = textBox;
                matrixSm.Children.Add(textBox);
            }
            else
            {
                TextBlock label = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 20,
                    Text = 0 + ""
                };
                label.SetValue(Grid.RowProperty, i);
                label.SetValue(Grid.ColumnProperty, j);


                _uiWMatrix[i, j] = label;
                matrixSm.Children.Add(label);
            }
        }

        /// <summary>
        /// Initialize UI part of distance matrix for 1 el
        /// </summary>
        /// <param name="i">row Id</param>
        /// <param name="j">column Id</param>
        private void InitDistUi(int i, int j)
        {
            if (j > i)
            {
                TextBox textBox = new TextBox
                {
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 20
                };
                textBox.TextChanged += DistMatrix_Changed;
                textBox.SetValue(Grid.RowProperty, i);
                textBox.SetValue(Grid.ColumnProperty, j);


                _uiDistMatrix[i, j] = textBox;
                matrixDist.Children.Add(textBox);
            }
            else
            {
                TextBlock label = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 20,
                    Text = 0 + ""
                };
                label.SetValue(Grid.RowProperty, i);
                label.SetValue(Grid.ColumnProperty, j);


                _uiDistMatrix[i, j] = label;
                matrixDist.Children.Add(label);
            }
        }

        /// <summary>
        /// Changes distance matrix element
        /// </summary>
        private void DistMatrix_Changed(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox) e.Source;
            int i = (int) textBox.GetValue(Grid.RowProperty);
            int j = (int) textBox.GetValue(Grid.ColumnProperty);
            int.TryParse(textBox.Text, out var edge);
            if (edge > 0)
            {
                _distMatrix[i, j] = edge;
                _distMatrix[j, i] = edge;
                TextBlock label = (TextBlock) _uiDistMatrix[j, i];
                label.Text = edge + "";
            }
        }

        /// <summary>
        /// Changes vertices count, creates appropriate matrixes for graph
        /// </summary>
        private void Count_Changed(object sender, TextChangedEventArgs e)
        {
            
                matrixSm.Children.RemoveRange(0, matrixSm.Children.Count);
                matrixDist.Children.RemoveRange(0, matrixDist.Children.Count);

                matrixSm.RowDefinitions.Clear();
                matrixSm.ColumnDefinitions.Clear();
                matrixDist.RowDefinitions.Clear();
                matrixDist.ColumnDefinitions.Clear();

                int.TryParse(Count.Text, out int vertCount);
                
                if (vertCount > 1 && vertCount <= maxCount)
                {
                    InitMatrix(vertCount);
                    for (int i = 0; i < vertCount; i++)
                    {
                        matrixSm.RowDefinitions.Add(new RowDefinition());
                        matrixSm.ColumnDefinitions.Add(new ColumnDefinition());
                        matrixDist.RowDefinitions.Add(new RowDefinition());
                        matrixDist.ColumnDefinitions.Add(new ColumnDefinition());
                    }


                    for (int i = 0; i < vertCount; i++)
                    {
                        for (int j = 0; j < vertCount; j++)
                        {
                            InitWUi(i, j);
                            InitDistUi(i, j);
                        }
                    }
                }
                else
                {
                    _vertsCount = 0;
                    // _distMatrix = new int[0, 0];
                    // _weightMatrix = new int[0, 0];
                }
            
        }

        /// <summary>
        /// Changes weight matrix element
        /// </summary>
        private void WMatrix_Changed(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox) e.Source;
            int i = (int) textBox.GetValue(Grid.RowProperty);
            int j = (int) textBox.GetValue(Grid.ColumnProperty);
            textBox.VerticalContentAlignment = VerticalAlignment.Top;
            int.TryParse(textBox.Text, out var edge);
            _weightMatrix[i, j] = edge;
        }

        /// <summary>
        /// Switches to next page
        /// </summary>
        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_vertsCount > 1 && _vertsCount <= maxCount)
                {
                    if (_destId != _startId && _startId >= 0 && _startId < _vertsCount && _destId >= 0 &&
                        _destId < _vertsCount)
                    {
                        CustomGraph customGraph = new CustomGraph(_weightMatrix, _distMatrix);
                        NavigationService.Navigate(new PrintGraph(customGraph, _startId, _destId));
                    }
                    else
                    {
                        MessageBox.Show("Incorrect start or finish Id!", "Program message");
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect vertices count!", "Program message");
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Changes start Id of path
        /// </summary>
        private void StartId_Changed(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(((TextBox) e.Source).Text, out _startId))
            {
                _startId = -1;
            }

            
        }

        /// <summary>
        /// Changes destination Id of path
        /// </summary>
        private void FinishId_Changed(object sender, TextChangedEventArgs e)
        {
            int.TryParse(((TextBox) e.Source).Text, out _destId);
        }

        /// <summary>
        /// Generates weight matrix 
        /// </summary>
        private void GenerateWight_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int maxRInt = 10;

            for (int i = 0; i < _vertsCount; i++)
            {
                for (int j = i + 1; j < _vertsCount; j++)
                {
                    int edgeWeight = random.Next(maxRInt);

                    TextBox textBox;
                    switch (random.Next(3))
                    {
                        case 0:
                            textBox = (TextBox) _uiWMatrix[i, j];
                            textBox.Text = edgeWeight + "";
                            textBox = (TextBox) _uiWMatrix[j, i];
                            textBox.Text = "0";
                            break;
                        case 1:
                            textBox = (TextBox) _uiWMatrix[j, i];
                            textBox.Text = edgeWeight + "";
                            textBox = (TextBox) _uiWMatrix[i, j];
                            textBox.Text = "0";
                            break;
                        case 2:
                            textBox = (TextBox) _uiWMatrix[j, i];
                            textBox.Text = edgeWeight + "";
                            textBox = (TextBox) _uiWMatrix[i, j];
                            textBox.Text = edgeWeight + "";
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Generates distance matrix
        /// </summary>
        private void GenerateDist_Click(object sender, RoutedEventArgs e)
        {
            int maxNumber = 10;
            Random random = new Random();

            for (int i = 0; i < _vertsCount; i++)
            {
                for (int j = i + 1; j < _vertsCount; j++)
                {
                    TextBox textBox = (TextBox) _uiDistMatrix[i, j];
                    textBox.Text = random.Next(maxNumber) + "";
                }
            }
        }
    }
}