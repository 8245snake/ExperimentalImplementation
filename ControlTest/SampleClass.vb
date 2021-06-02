Option Explicit On
Option Strict On

Public Class SampleClass

    Public Shared Function CreateCountries() As List(Of Country)
        Dim countries As List(Of Country) = New List(Of Country)()

        Dim country As Country = New Country With {.Name = "アメリカ"}
        country.Cities.Add(New City With {.Name = "ニューヨーク"})
        country.Cities.Add(New City With {.Name = "ロサンゼルス"})
        country.Cities.Add(New City With {.Name = "シカゴ"})
        country.Cities.Add(New City With {.Name = "ヒューストン"})
        countries.Add(country)

        country = New Country With {.Name = "日本"}
        country.Cities.Add(New City With {.Name = "東京都"})
        country.Cities.Add(New City With {.Name = "神奈川県"})
        country.Cities.Add(New City With {.Name = "大阪府"})
        countries.Add(country)

        country = New Country With {.Name = "中国"}
        country.Cities.Add(New City With {.Name = "広州市"})
        country.Cities.Add(New City With {.Name = "上海市"})
        country.Cities.Add(New City With {.Name = "北京市"})
        country.Cities.Add(New City With {.Name = "天津市"})
        countries.Add(country)

        Return countries
    End Function

End Class

Public Class Country
    Public Property Name As String
    Public Cities As List(Of City) = New List(Of City)()
End Class


Public Class City
    Public Property Name As String
End Class