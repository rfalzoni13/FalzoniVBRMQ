Imports FalzoniVBRMQ.Common.Config
Imports FalzoniVBRMQ.Producer.Workers
Imports RabbitMQ.Client

Module Program

    Sub Main()
        Try
            Console.WriteLine("Criando novo producer do exchange Direct")
            Dim p1 = New ProducerWorker()


            Console.WriteLine("Enviando mensagem")
            p1.Produce("Teste de mensagem Direct", RabbitMQAttributes.EXG_DIRECT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_DATA, ExchangeType.Direct, RabbitMQAttributes.RK_PRODUCT_DATA)
            Console.WriteLine("Processo concluído!")


            Console.WriteLine("Criando novo producer do exchange Topic")
            Dim p2 = New ProducerWorker()

            Console.WriteLine("Enviando mensagem")
            p2.Produce("Teste de mensagem Topic", RabbitMQAttributes.EXG_TOPIC_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, ExchangeType.Topic, RabbitMQAttributes.RK_PRODUCT_LOG)
            p2.Produce("Teste de mensagem Topic para todos", RabbitMQAttributes.EXG_TOPIC_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, ExchangeType.Topic, RabbitMQAttributes.RK_PRODUCT_ALL)
            Console.WriteLine("Processo concluído!")


            Console.WriteLine("Criando novo producer do exchange Fanout")
            Dim p3 = New ProducerWorker()

            Console.WriteLine("Enviando mensagem")
            p3.Produce("Teste de mensagem Fanout", RabbitMQAttributes.EXG_FANOUT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, ExchangeType.Fanout, String.Empty)
            Console.WriteLine("Processo concluído!")

            Console.ReadLine()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Module
