using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace GetUnusedSkills
{
    public partial class Form1 : Form
    {
        public Form1 ( )
        {
            InitializeComponent( );
        }
        public static string Reverse ( string s )
        {
            char [ ] charArray = s.ToCharArray( );
            Array.Reverse( charArray );
            return new string( charArray );
        }
        private void button1_Click ( object sender , EventArgs e )
        {
            List<string> AllStringData = new List<string>( );
            List<string> AllStringData2 = new List<string>( );



            string Regex1 = @"\[(\w\w\w\w)\]";
            string Regex2 = @"\[(\w\w\w\w)\]";

            foreach ( string file in Directory.GetFiles( textBox1.Text , "*.xxx" , SearchOption.AllDirectories ) )
            {
                    AllStringData.AddRange( File.ReadAllLines( file ) );
            }

            foreach ( string file in Directory.GetFiles( textBox1.Text , "*.txt" , SearchOption.AllDirectories ) )
            {
                AllStringData2.AddRange( File.ReadAllLines( file ) );
            }



            MessageBox.Show( "Ab:" + AllStringData.Count.ToString( ) );

            List<string> AbilCodes = new List<string>( );


            foreach ( string str in AllStringData )
            {
                Match RegexMatch = Regex.Match( str , Regex2 );
                if ( RegexMatch.Success )
                {
                    AbilCodes.Add( RegexMatch.Groups [ 1 ].Value );
                }
            }

            MessageBox.Show( "Abils:" + AbilCodes.Count.ToString( ) );

            foreach ( string str in AllStringData2 )
            {
                Match RegexMatch = Regex.Match( str , Regex1 );
                if ( RegexMatch.Success )
                {
                    for ( int i = 0 ; i < AbilCodes.Count ; i++ )
                    {
                        string value = RegexMatch.Groups [ 1 ].Value;

                        if ( AbilCodes [ i ] == value )
                        {
                            AbilCodes.RemoveAt( i );
                            i--;
                        }

                        value = Reverse( value );

                        if ( AbilCodes [ i ] == value )
                        {
                            AbilCodes.RemoveAt( i );
                            i--;
                        }
                    }
                }
            }


            MessageBox.Show( "Abils:" + AbilCodes.Count.ToString( ) );


            File.WriteAllLines( "outdata.txt" , AbilCodes.ToArray( ) );

        }
    }
}
