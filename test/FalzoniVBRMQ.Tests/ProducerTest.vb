Imports System.Linq.Expressions
Imports FalzoniVBRMQ.Common.Config
Imports FalzoniVBRMQ.Consumer.Workers
Imports FalzoniVBRMQ.Producer.Workers
Imports FalzoniVBRMQ.Tests.Utils
Imports Moq

<TestClass()> Public Class ProducerTest

    Private _mock As Mock(Of ProducerWorker)
    Private _predicate As Expression(Of Action(Of ProducerWorker))

    <TestInitialize()> Public Sub TestInitialize()
        _mock = New Mock(Of ProducerWorker)()

        _predicate = Sub(m) m.Produce(It.IsAny(Of String)(), It.IsAny(Of String)(), It.IsAny(Of String)(), It.IsAny(Of String)(), It.IsAny(Of String)())
    End Sub

    <TestMethod()> Public Sub TestRunConsumerSingle_Topic_Success()

        Dim obj As ProducerWorker = TestUtils(Of ProducerWorker, String).SetupMock(_mock, _predicate)

        obj.Produce("Teste de mensagem Direct", RabbitMQAttributes.EXG_DIRECT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_DATA, "direct", RabbitMQAttributes.RK_PRODUCT_DATA)

        Assert.IsTrue(True)
    End Sub

    <ExpectedException(GetType(AssertFailedException))>
    <TestMethod()> Public Sub TestRunProducer_Direct_Fail()
        Try
            Dim obj As ProducerWorker = TestUtils(Of ProducerWorker, String).SetupExceptionMock(_mock, _predicate)
            obj.Produce("Teste de mensagem Direct", RabbitMQAttributes.EXG_DIRECT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_DATA, "direct", RabbitMQAttributes.RK_PRODUCT_DATA)
        Catch ex As Exception
            Assert.Fail()
        End Try
    End Sub

    <TestMethod()> Public Sub TestRunProducer_Fanout_Success()

        Dim obj As ProducerWorker = TestUtils(Of ProducerWorker, String).SetupMock(_mock, _predicate)

        obj.Produce("Teste de mensagem Fanout", RabbitMQAttributes.EXG_FANOUT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, "fanout", String.Empty)

        Assert.IsTrue(True)
    End Sub

    <ExpectedException(GetType(AssertFailedException))>
    <TestMethod()> Public Sub TestRunProducer_Fanout_Fail()
        Try
            Dim obj As ProducerWorker = TestUtils(Of ProducerWorker, String).SetupExceptionMock(_mock, _predicate)
            obj.Produce("Teste de mensagem Fanout", RabbitMQAttributes.EXG_FANOUT_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, "fanout", String.Empty)
        Catch ex As Exception
            Assert.Fail()
        End Try
    End Sub

    <TestMethod()> Public Sub TestRunProducer_Topic_Success()

        Dim obj As ProducerWorker = TestUtils(Of ProducerWorker, String).SetupMock(_mock, _predicate)

        obj.Produce("Teste de mensagem Topic", RabbitMQAttributes.EXG_TOPIC_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, "topic", RabbitMQAttributes.RK_PRODUCT_LOG)

        Assert.IsTrue(True)
    End Sub

    <ExpectedException(GetType(AssertFailedException))>
    <TestMethod()> Public Sub TestRunProducer_Topic_Fail()
        Try
            Dim obj As ProducerWorker = TestUtils(Of ProducerWorker, String).SetupExceptionMock(_mock, _predicate)
            obj.Produce("Teste de mensagem Topic", RabbitMQAttributes.EXG_TOPIC_NAME, RabbitMQAttributes.QUEUE_PRODUCT_LOG, "topic", RabbitMQAttributes.RK_PRODUCT_LOG)
        Catch ex As Exception
            Assert.Fail()
        End Try
    End Sub
End Class