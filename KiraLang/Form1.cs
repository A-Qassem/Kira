using System;
using System.Drawing;
using System.Windows.Forms;
using ScintillaNET;
using com.calitha.goldparser;

namespace KiraLang
{
    public partial class UI_Form : Form
    {
        private Scintilla editor;
        private MyParser parser;

        public UI_Form()
        {
            InitializeComponent();
            InitializeEditor();

            syntaxErrorBox = new ListBox
            {
                Font = new Font("Consolas", 10),
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.DarkRed,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill
            };

            lexicalBox = new ListBox
            {
                Font = new Font("Consolas", 15),
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.DarkBlue,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill
            };

            // Panels with labels for each ListBox
            var syntaxPanel = new Panel { Dock = DockStyle.Fill };
            var syntaxLabel = new Label
            {
                Text = "Syntax Errors",
                Font = new Font("Consolas", 10, FontStyle.Bold),
                Dock = DockStyle.Top,
                ForeColor = Color.Maroon,
                Height = 25
            };
            syntaxPanel.Controls.Add(syntaxErrorBox);
            syntaxPanel.Controls.Add(syntaxLabel);

            var lexicalPanel = new Panel { Dock = DockStyle.Fill };
            var lexicalLabel = new Label
            {
                Text = "Lexical box",
                Font = new Font("Consolas", 10, FontStyle.Bold),
                Dock = DockStyle.Top,
                ForeColor = Color.Navy,
                Height = 25
            };
            lexicalPanel.Controls.Add(lexicalBox);
            lexicalPanel.Controls.Add(lexicalLabel);

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            table.Controls.Add(syntaxPanel, 0, 0);
            table.Controls.Add(lexicalPanel, 0, 1);

            var sidePanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 450,
                BackColor = Color.Gainsboro
            };
            sidePanel.Controls.Add(table);

            this.Controls.Add(sidePanel);
            this.WindowState = FormWindowState.Maximized;

            parser = new MyParser("C:\\Users\\Ahmed\\source\\repos\\KiraLang\\KiraLang\\bin\\Debug\\KiraLang.cgt", syntaxErrorBox, lexicalBox);
        }

        private void InitializeEditor()
        {
            editor = new Scintilla
            {
                Dock = DockStyle.Fill
            };

            editor.Styles[Style.Default].Font = "Consolas";
            editor.Styles[Style.Default].Size = 12;
            editor.StyleClearAll();

            editor.Styles[1].ForeColor = Color.Blue;
            editor.Styles[2].ForeColor = Color.Orange;
            editor.Styles[3].ForeColor = Color.Green;
            editor.Styles[4].ForeColor = Color.Red;

            editor.Lexer = Lexer.Null;
            editor.Margins[0].Width = 30;

            editor.TextChanged += (sender, e) => HighlightAll();

            this.Controls.Add(editor);
        }

        private void HighlightAll()
        {
            editor.TextChanged -= Editor_TextChanged;

            int length = editor.TextLength;
            editor.StartStyling(0);
            editor.SetStyling(length, 0);

            string[] keywords = { "def", "if", "then", "else", "while", "print" };
            foreach (var word in keywords)
            {
                HighlightWord(word, 1);
            }

            HighlightNumbers();
            HighlightComments();
            HighlightStrings();

            editor.TextChanged += Editor_TextChanged;
        }

        private void HighlightWord(string word, int style)
        {
            int startPos = 0;
            int length = editor.TextLength;

            while (startPos < length)
            {
                int index = editor.Text.IndexOf(word, startPos, StringComparison.OrdinalIgnoreCase);
                if (index == -1) break;

                bool isWordStart = (index == 0) || !Char.IsLetterOrDigit(editor.Text[index - 1]);
                bool isWordEnd = (index + word.Length == length) || !Char.IsLetterOrDigit(editor.Text[index + word.Length]);

                if (isWordStart && isWordEnd)
                {
                    editor.StartStyling(index);
                    editor.SetStyling(word.Length, style);
                }

                startPos = index + word.Length;
            }
        }

        private void HighlightNumbers()
        {
            int pos = 0;
            int length = editor.TextLength;

            while (pos < length)
            {
                if (Char.IsDigit(editor.Text[pos]))
                {
                    int start = pos;
                    while (pos < length && Char.IsDigit(editor.Text[pos]))
                        pos++;

                    editor.StartStyling(start);
                    editor.SetStyling(pos - start, 2);
                }
                else
                {
                    pos++;
                }
            }
        }

        private void HighlightComments()
        {
            int pos = 0;
            int length = editor.TextLength;

            while (pos < length)
            {
                if (editor.Text[pos] == '#')
                {
                    int start = pos;
                    while (pos < length && editor.Text[pos] != '\n')
                        pos++;

                    editor.StartStyling(start);
                    editor.SetStyling(pos - start, 3);
                }
                else
                {
                    pos++;
                }
            }
        }

        private void HighlightStrings()
        {
            int pos = 0;
            int length = editor.TextLength;

            while (pos < length)
            {
                if (editor.Text[pos] == '"')
                {
                    int start = pos++;
                    while (pos < length && editor.Text[pos] != '"')
                        pos++;
                    if (pos < length) pos++;

                    editor.StartStyling(start);
                    editor.SetStyling(pos - start, 4);
                }
                else
                {
                    pos++;
                }
            }
        }

        private void Editor_TextChanged(object sender, EventArgs e)
        {
            syntaxErrorBox.Items.Clear();
            lexicalBox.Items.Clear();
            parser.Parse(editor.Text);
        }
    }
}
