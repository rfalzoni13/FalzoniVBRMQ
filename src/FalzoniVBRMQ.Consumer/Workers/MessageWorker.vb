Namespace Workers
    Public Class MessageWorker
        Shared Sub ConsumeSingleMessage(consumer As ConsumerWorker, queueName As String)
            Dim messages As List(Of String) = New List(Of String)()

            Dim messageCount As UInteger = consumer.GetMessageCount(queueName)

            Dim i = 0
            For i = 0 To messageCount - 1
                messages.AddRange(consumer.Consume(queueName))
            Next

            PrintMessages(messages)
        End Sub

        Shared Sub ConsumeAllMessages(consumer As ConsumerWorker, queues As String())
            Dim messages As List(Of String) = New List(Of String)()

            Dim i = 0
            For i = 0 To queues.Length - 1
                messages.AddRange(consumer.ConsumeAll(queues(i)))
            Next

            PrintMessages(messages)
        End Sub

        Shared Async Function ConsumeAllMessagesAsync(consumer As ConsumerWorker, queues As String()) As Task
            Dim messages As List(Of String) = New List(Of String)()

            Dim i = 0
            For i = 0 To queues.Length - 1
                messages.AddRange(Await consumer.ConsumeAllAsync(queues(i)))
            Next

            PrintMessages(messages)
        End Function

        Private Shared Sub PrintMessages(messages As List(Of String))
            For Each message In messages
                Console.WriteLine("Exibida a seguinte mensagem: " + If(Not String.IsNullOrEmpty(message), message, "Vazia"))
            Next
        End Sub
    End Class

End Namespace
