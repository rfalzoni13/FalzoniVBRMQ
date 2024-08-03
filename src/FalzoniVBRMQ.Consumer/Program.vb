Imports FalzoniVBRMQ.Common.Config
Imports FalzoniVBRMQ.Consumer.Workers

Module Program

    Sub Main()
        Try
            Console.WriteLine("Criando novo consumer")
            Dim Consumer = New ConsumerWorker()

            Console.WriteLine("Cosumindo mensagens")

            ' Consumo de todas as mensagens
            MessageWorker.ConsumeAllMessages(Consumer, New String() {RabbitMQAttributes.QUEUE_PRODUCT_DATA, RabbitMQAttributes.QUEUE_PRODUCT_LOG})
            'MessageWorker.ConsumeAllMessagesAsync(Consumer, New String() {RabbitMQAttributes.QUEUE_PRODUCT_DATA, RabbitMQAttributes.QUEUE_PRODUCT_LOG})

            ' Consumo de mensagens individuais
            'MessageWorker.ConsumeSingleMessage(consumer, RabbitMQAttributes.QUEUE_PRODUCT_DATA)
            'MessageWorker.ConsumeSingleMessage(consumer, RabbitMQAttributes.QUEUE_PRODUCT_LOG)

            Console.ReadLine()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Module
