Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json

Public NotInheritable Class Utilities
    Public Shared Sub CreateCsvFile(Of T)(path As String, values As List(Of T))
        Using streamWriter = New StreamWriter(path)
            Dim config = New CsvHelper.Configuration.CsvConfiguration(Globalization.CultureInfo.InvariantCulture) With {
            .ShouldQuote = Function(args) True
        }
            Using csvWriter = New CsvHelper.CsvWriter(streamWriter, config)
                csvWriter.WriteRecords(values)
            End Using
        End Using
    End Sub

    Public Shared Function SendGetRequest(Of T As Class)(url As String, Optional headers As Dictionary(Of String, String) = Nothing, Optional contentType As String = "application/json") As T
        Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        req.Method = "GET"
        req.ContentType = contentType

        If headers IsNot Nothing Then
            For Each kvp As KeyValuePair(Of String, String) In headers
                req.Headers.Add(kvp.Key, kvp.Value)
            Next
        End If

        Dim res As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
        Dim rs = New StreamReader(res.GetResponseStream())
        Dim jsonString = rs.ReadToEnd()
        Return JsonConvert.DeserializeObject(Of T)(jsonString)
    End Function
    Public Shared Sub SendRequest(url As String, method As String, Optional headers As Dictionary(Of String, String) = Nothing)
        Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        req.Method = method
        req.ContentType = "application/json"

        If headers IsNot Nothing Then
            For Each kvp As KeyValuePair(Of String, String) In headers
                req.Headers.Add(kvp.Key, kvp.Value)
            Next
        End If

        Dim res As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
        Dim rs = New StreamReader(res.GetResponseStream())
        Dim jsonString = rs.ReadToEnd()
    End Sub
    Public Shared Sub SendRequest(Of T As Class)(url As String, method As String, body As T, Optional headers As Dictionary(Of String, String) = Nothing, Optional contentType As String = "application/vnd.oracle.adf.resourcecollection+json")
        Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        req.Method = method
        req.ContentType = contentType

        If headers IsNot Nothing Then
            For Each kvp As KeyValuePair(Of String, String) In headers
                req.Headers.Add(kvp.Key, kvp.Value)
            Next
        End If

        Dim postbody = JsonConvert.SerializeObject(body, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postbody)

        req.ContentLength = byteArray.Length
        Dim dataStream As Stream = req.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()

        Dim res As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
    End Sub
End Class
