Imports System.Linq.Expressions
Imports Moq

Namespace Utils
    Public Class TestUtils(Of T As Class, TResult As Class)
        Public Shared Function SetupMock(mock As Mock(Of T), predicate As Expression(Of Action(Of T))) As T
            mock.Setup(predicate)

            Return mock.Object
        End Function

        Public Shared Function SetupReturnMock(mock As Mock(Of T), predicate As Expression(Of Func(Of T, TResult)), ret As TResult) As T
            mock.Setup(predicate).Returns(ret)

            Return mock.Object
        End Function

        'Public Shared Function SetupReturnMockAsync(mock As Mock(Of T), predicate As Expression(Of Func(Of T, Task)), ret As TResult) As T
        '    mock.Setup(predicate).Returns(Task.FromResult(Of TResult)(ret))

        '    Return mock.Object
        'End Function

        Public Shared Function SetupExceptionMock(mock As Mock(Of T), predicate As Expression(Of Action(Of T))) As T
            mock.Setup(predicate).Throws(New Exception())

            Return mock.Object
        End Function

        Public Shared Function SetupExceptionReturnMock(mock As Mock(Of T), predicate As Expression(Of Func(Of T, TResult))) As T
            mock.Setup(predicate).Throws(New Exception())

            Return mock.Object
        End Function

        'Public Shared Function SetupExceptionReturnMockAsync(mock As Mock(Of T), predicate As Expression(Of Func(Of T, Task))) As T
        '    mock.Setup(predicate).Throws(New Exception())

        '    Return mock.Object
        'End Function
    End Class
End Namespace
