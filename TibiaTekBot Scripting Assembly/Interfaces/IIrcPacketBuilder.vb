﻿'    Copyright (C) 2007 TibiaTek Development Team
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

Public Interface IIrcPacketBuilder
    Inherits IPacketBuilder

#Region " Enumerations "
    Enum Destinataries
        Player = 0
        All
        Party
        Guild
        World
    End Enum
#End Region

#Region " Methods "
    Sub BroadcastLocation(ByVal EntityName As String, ByVal Location As ITibia.LocationDefinition)
    Sub BroadcastStats(ByVal EntityName As String, ByVal Level As UInteger, ByVal Health As UInteger, ByVal Mana As UInteger)
    Sub BroadcastEntityStats(ByVal EntityName As String, ByVal HealthPercent As Byte, ByVal Location As ITibia.LocationDefinition)
    Overloads Sub Send(ByVal Destinatary As IIrcPacketBuilder.Destinataries)
    Overloads Sub Send(ByVal Destinatary As String)
#End Region

End Interface