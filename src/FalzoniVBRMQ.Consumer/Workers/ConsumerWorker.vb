Imports System.Text
Imports System.Threading
Imports FalzoniVBRMQ.Common.Config
Imports RabbitMQ.Client
Imports RabbitMQ.Client.Events

Namespace Workers
    Public Class ConsumerWorker
        Private Property _messages As List(Of String)

        Public Overridable Function GetMessageCount(queueName As String) As UInteger
            Dim channel As IModel = RabbitMQConfig.GetChannel()
            Return channel.MessageCount(queueName)
        End Function

        Public Overridable Function Consume(queueName As String) As List(Of String)
            _messages = New List(Of String)()

            Dim channel As IModel = RabbitMQConfig.GetChannel()

            Dim result As BasicGetResult = channel.BasicGet(queueName, True)

            If result Is Nothing Then
                _messages.Add("Nenhuma mensagem nesta fila")
                Return _messages
            End If

            Dim props As IBasicProperties = result.BasicProperties
            Dim body As ReadOnlyMemory(Of Byte) = result.Body

            Dim message = Encoding.UTF8.GetString(body.ToArray())
            _messages.Add(message)

            ' acknowledge receipt of the message
            channel.BasicAck(result.DeliveryTag, False)

            Return _messages
        End Function

        Public Overridable Function ConsumeAll(queueName As String) As List(Of String)
            _messages = New List(Of String)()
            Dim channel As IModel = RabbitMQConfig.GetChannel()
            Using signal = New ManualResetEvent(False)
                Dim consumer = New EventingBasicConsumer(channel)
                AddHandler consumer.Received,
                    Sub(ch As IBasicConsumer, ea As BasicDeliverEventArgs)
                        Dim body = ea.Body.ToArray()
                        ' copy Or deserialise the payload
                        ' And process the message
                        ' ...

                        Dim message = Encoding.UTF8.GetString(body)

                        _messages.Add(message)

                        channel.BasicAck(ea.DeliveryTag, False)

                        signal.Set()
                    End Sub
                ' this consumer tag identifies the subscription
                ' when it has to be cancelled
                Dim consumerTag As String = channel.BasicConsume(queueName, False, consumer)

                signal.WaitOne(TimeSpan.FromSeconds(30))

                channel.BasicCancel(consumerTag)
            End Using

            Return _messages
        End Function

        Public Overridable Async Function ConsumeAllAsync(queueName As String) As Task(Of List(Of String))
            _messages = New List(Of String)()
            Dim channel As IModel = RabbitMQConfig.GetChannel()
            Using signal = New ManualResetEvent(False)
                Dim consumer = New AsyncEventingBasicConsumer(channel)
                AddHandler consumer.Received,
                    Async Function(ch As IBasicConsumer, ea As BasicDeliverEventArgs)
                        Dim body = ea.Body.ToArray()
                        ' copy Or deserialise the payload
                        ' And process the message
                        ' ...

                        Dim message = Encoding.UTF8.GetString(body)

                        _messages.Add(message)

                        channel.BasicAck(ea.DeliveryTag, False)

                        signal.Set()

                        Await Task.Yield()
                    End Function
                ' this consumer tag identifies the subscription
                ' when it has to be cancelled
                Dim consumerTag As String = channel.BasicConsume(queueName, False, consumer)

                signal.WaitOne(TimeSpan.FromSeconds(30))

                channel.BasicCancel(consumerTag)
            End Using

            Return Await Task.FromResult(_messages)
        End Function
    End Class
End Namespace
