Imports System
Imports System.Collections.Generic

Namespace Services

    ''' <summary>
    ''' Observerを弱参照で扱うObservebleパターン
    ''' </summary>
    ''' <typeparam name="T">任意の型</typeparam>
    Public Class WeakObserveble(Of T)

        Protected _Observers As List(Of WeakReference) = New List(Of WeakReference)

        ''' <summary>
        ''' 購読リストに追加する
        ''' </summary>
        ''' <param name="observer">オブザーバ</param>
        Public Sub Subscribe(ByVal observer As IObserver(Of T))
            Me._Observers.Add(New WeakReference(observer))
        End Sub

        ''' <summary>
        ''' 購読を解除する
        ''' </summary>
        ''' <param name="observer">オブザーバ</param>
        Public Sub Unsubscribe(ByVal observer As IObserver(Of T))
            Dim delTarget As WeakReference = Nothing
            Dim disposedReferences = New List(Of WeakReference)

            For Each reference As WeakReference In Me._Observers
                Dim target = TryCast(reference.Target, IObserver(Of T))
                If (target IsNot Nothing) Then
                    If target Is observer Then
                        delTarget = reference
                    End If
                Else
                    ' 参照切れ
                    disposedReferences.Add(reference)
                End If
            Next

            If (delTarget IsNot Nothing) Then
                If disposedReferences.Contains(delTarget) Then
                    disposedReferences.Remove(delTarget)
                End If

                Me._Observers.Remove(delTarget)
            End If

            Me.RemoveRange(disposedReferences)
        End Sub

        ''' <summary>
        ''' OnNextを呼ぶ
        ''' </summary>
        ''' <param name="observer">オブザーバ</param>
        Protected Overridable Sub SendOnNext(ByVal observer As IObserver(Of T))
            observer.OnNext(Nothing)
        End Sub

        ''' <summary>
        ''' OnErrorを呼ぶ
        ''' </summary>
        ''' <param name="observer">オブザーバ</param>
        Protected Overridable Sub SendOnError(ByVal observer As IObserver(Of T))
            observer.OnError(New Exception)
        End Sub

        ''' <summary>
        ''' OnCompletedを呼ぶ
        ''' </summary>
        ''' <param name="observer">オブザーバ</param>
        Protected Overridable Sub SendOnCompleted(ByVal observer As IObserver(Of T))
            observer.OnCompleted()
        End Sub

        ''' <summary>
        ''' オブザーバに向けて通知を発行する
        ''' </summary>
        Public Overridable Sub Notify()
            Me.NotifyInternal(AddressOf SendOnNext)
        End Sub

        ''' <summary>
        ''' オブザーバに向けて通知を発行する内部処理
        ''' </summary>
        Protected Sub NotifyInternal(ByVal notifyFunc As Action(Of IObserver(Of T)))
            Dim disposedReferences = New List(Of WeakReference)
            For Each reference As WeakReference In Me._Observers
                Dim observer = TryCast(reference.Target, IObserver(Of T))
                If observer IsNot Nothing Then
                    notifyFunc(observer)
                Else
                    disposedReferences.Add(reference)
                End If
            Next
            ' 参照切れを削除
            Me.RemoveRange(disposedReferences)
        End Sub

        ''' <summary>
        ''' 購読リストから削除する
        ''' </summary>
        ''' <param name="delList">削除リスト</param>
        Private Sub RemoveRange(ByVal delList As IEnumerable(Of WeakReference))
            For Each item In delList
                Me._Observers.Remove(item)
            Next
        End Sub
    End Class
End Namespace