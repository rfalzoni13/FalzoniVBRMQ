Imports FalzoniVBRMQ.Common.Config
Imports RabbitMQ.Client

Namespace Workers
    Public Class ProducerWorker
        Public Overridable Sub Produce(message As String, exchangeName As String, queueName As String, exchangeType As String, routingKey As String)
            Dim channel As IModel = RabbitMQConfig.GetChannel(exchangeName, exchangeType, queueName, routingKey)

            Dim messageBodyBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(message)

            ' Opções adicionais
            'Dim props As IBasicProperties = channel.CreateBasicProperties()
            'props.ContentType = "text/plain"
            'props.DeliveryMode = 2
            'props.Headers = New Dictionary(Of String, Object)()
            'props.Headers.Add("parameter1", "teste")
            'props.Headers.Add("parameter2", "teste2")
            'props.Expiration = "36000000"
            'channel.BasicPublish(exchangeName, routingKey, props, messageBodyBytes)

            channel.BasicPublish(exchangeName, routingKey, Nothing, messageBodyBytes)
        End Sub
    End Class
End Namespace
