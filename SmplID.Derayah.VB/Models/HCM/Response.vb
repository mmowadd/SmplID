Namespace Models.HCM
    Public Class Response(Of T As Class)
        Public Property items As List(Of T)
    End Class

    Public Class ScimResponse(Of T As Class)
        Public Property Resources As List(Of T)
    End Class
End Namespace
