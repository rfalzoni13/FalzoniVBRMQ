Imports RabbitMQ.Client

Namespace Config
    Public Class RabbitMQConfig
        Public Shared Function GetChannel(Optional consumersAsync As Boolean = False) As IModel
            Dim conn As IConnection = GetConnection(consumersAsync)

            Dim Channel As IModel = conn.CreateModel()

            Return Channel
        End Function

        Public Shared Function GetChannel(exchangeName As String, exchangeType As String, queueName As String, routingKey As String) As IModel
            Dim conn As IConnection = GetConnection()

            Dim Channel As IModel = conn.CreateModel()

            Channel.ExchangeDeclare(exchangeName, exchangeType, False, False, Nothing)
            Channel.QueueDeclare(queueName, False, False, False, Nothing)
            Channel.QueueBind(queueName, exchangeName, routingKey, Nothing)

            'Channel.QueueDeclareNoWait(queueName, True, false, false, Nothing)

            Return Channel
        End Function

        Private Shared Function GetConnection(Optional consumersAsync As Boolean = False) As IConnection
            Dim factory As ConnectionFactory = New ConnectionFactory()

            factory.UserName = "guest"
            factory.Password = "guest"
            factory.VirtualHost = "/"
            factory.HostName = "localhost"
            factory.Port = 5672

            factory.DispatchConsumersAsync = consumersAsync

            Dim conn As IConnection = factory.CreateConnection()
            Return conn
        End Function

    End Class
End Namespace
