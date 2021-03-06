'    Copyright (C) 2007 TibiaTek Development Team
'
'    This file is part of TibiaTek Bot.
'
'    TibiaTek Bot is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    TibiaTek Bot is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with TibiaTek Bot. If not, see http://www.gnu.org/licenses/gpl.txt
'    or write to the Free Software Foundation, 59 Temple Place - Suite 330,
'    Boston, MA 02111-1307, USA.

Imports System.Windows.Forms, TibiaTekBot.KernelModule, System.Xml, System.Text.RegularExpressions

Public Class frmLoginSelectDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Dim MatchObj As Match = Regex.Match(loginServers.Text, "^([^:]+):(\d+)$")
            If MatchObj.Success Then
                Kernel.LoginServer = MatchObj.Groups(1).Value
                Kernel.LoginPort = CInt(MatchObj.Groups(2).Value)
                Dim xmlDocument As New System.Xml.XmlDocument()
                xmlDocument.Load(GetConfigurationDirectory() & "\Data.xml")

                Dim Add As Boolean = True
                Dim Node As XmlNode = Nothing
                For Each Node In xmlDocument.GetElementsByTagName("Address")
                    If String.Compare(Node.InnerText, loginServers.Text) = 0 Then
                        Add = False
                        Exit For
                    End If
                Next
                If Add Then
                    Dim xmlNewNode As XmlElement = xmlDocument.CreateElement("Address")
                    xmlNewNode.InnerText = loginServers.Text
                    xmlDocument.Item("Client").Item("Addresses").InsertBefore(xmlNewNode, xmlDocument.Item("Client").Item("Addresses").FirstChild)
                ElseIf Not Node Is Nothing Then
                    Dim xmlNewNode As XmlElement = xmlDocument.CreateElement("Address")
                    xmlNewNode.InnerText = loginServers.Text
                    xmlDocument.Item("Client").Item("Addresses").InsertBefore(Node.Clone(), xmlDocument.Item("Client").Item("Addresses").FirstChild)
                    xmlDocument.Item("Client").Item("Addresses").RemoveChild(Node)

                End If
                xmlDocument.Save(GetConfigurationDirectory() & "\Data.xml")
            Else
                Dim Result As DialogResult
                Result = MessageBox.Show("Entry is not of the form hostname:port.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                If Result = Windows.Forms.DialogResult.Cancel Then
                    End
                Else
                    Exit Sub
                End If
            End If
            Kernel.IsOpenTibiaServer = privateServer.Checked
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Catch Ex As Exception
            ShowError(Ex)
            End
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Try
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Private Sub frmLoginSelectDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim xmlDocument As New System.Xml.XmlDocument()
            xmlDocument.Load(GetConfigurationDirectory() & "\Data.xml")
            For Each Node As XmlNode In xmlDocument.Item("Client").Item("Addresses")
                loginServers.Items.Add(Node.InnerText)
            Next
            If loginServers.Items.Count > 0 Then loginServers.SelectedIndex = 0
        Catch Ex As Exception
            ShowError(Ex)
            End
        End Try
    End Sub

    Private Sub SelectLoginHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectLoginHelp.Click
        MessageBox.Show("To login you must first select a login server from the list and click OK." & vbCrLf & _
                        "Remember all login servers must be written in the correct format: server:port." & vbCrLf & vbCrLf & _
                        "How to login to an OT Server" & vbCrLf & _
                        "1. Write the login server address." & vbCrLf & _
                        "2. Mark the OT Server checkbox." & vbCrLf & _
                        "3. Click OK." & vbCrLf & vbCrLf & _
                        "Warning: do not attempt to use an IP Changer with TibiaTek Bot." & vbCrLf & vbCrLf & _
                        "Remember:" & vbCrLf & _
                        "Sometimes Login Servers are down and you won't be able to login, if that is the case," & vbCrLf & _
                        "select another login server from the list.", "Select a Login Server Help", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
