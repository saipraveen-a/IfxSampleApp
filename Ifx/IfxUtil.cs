using Microsoft.Cloud.InstrumentationFramework;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleIfxApp.Ifx
{
    public class IfxUtil
    {
        public static void IfxObjectSamples()
        {

            // Log a text property.
            IfxObject.LogPropertyChange(
                "Book",                                   // The class name of the object instance.
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "Author",                                 // The name of logged property.
                "Margaret Mitchell");                     // The property value to be logged.

            // Log a double property.
            IfxObject.LogPropertyChange(
                "Book",                                   // The class name of the object instance.
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "Price",                                  // The name of logged property.
                9.99);                                    // The property value to be logged.

            // Log a int32 property.
            IfxObject.LogPropertyChange(
                "Book",                                   // The class name of the object instance.
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "Pages",                                  // The name of logged property.
                960);                                     // The property value to be logged.

            // Log a int64 property.
            IfxObject.LogPropertyChange(
                "Book",                                   // The class name of the object instance.
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "CheckinTimestampUtc",                    // The name of logged property.
                DateTime.UtcNow.ToFileTime());            // The property value to be logged.

            // Log a bool property.
            IfxObject.LogPropertyChange(
                "Book",                                   // The class name of the object instance.
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "Avaible In Stock",                       // The name of logged property.
                true);                                    // The property value to be logged.

            // Log begin of an object reference.
            IfxObject.LogObjectReferenceBegin(
                "Container",                              // The class name of the object instance. 
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "Node",                                   // The class name of the target object instance of the reference.
                "C8A640DD-F25E-4CA1-9265-1AC2628D0190",   // The unique identifier of the target object instance of the reference.
                "Container to Node reference");           // The name of logged reference.

            // Log end of an object reference.
            IfxObject.LogObjectReferenceEnd(
                "Container",                              // The class name of the object instance. 
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "Node",                                   // The class name of the target object instance of the reference.
                "C8A640DD-F25E-4CA1-9265-1AC2628D0190",   // The unique identifier of the target object instance of the reference.
                "Container to Node reference");           // The name of logged reference.

            // Extensibility sample

            /*
             * struct MyDoubleObjectPropertyEvent:Ifx.ObjectPropertySchema<double>
             * {
             *     10: required wstring PartCField1;
             *     20: required wstring PartCField2;
             * };
             */
            var myDoubleObjectPropertyEventBlob = new SampleIfx.MyDoubleObjectPropertyEvent
            {
                PartCField1 = "I'm a property",
                PartCField2 = "so am I"
            };

            IfxObject.LogExtendedPropertyChange<double, SampleIfx.MyDoubleObjectPropertyEvent>(
                "Book",                                   // The class name of the object instance.
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "Price",                                  // The name of logged property.
                9.99,                                     // The property value to be logged.
                myDoubleObjectPropertyEventBlob);         // partc blob.


            /* 
             * struct MyGenericObjectPropertyEvent<T>:Ifx.ObjectPropertySchema<T>
             * {
             *     10: required wstring PartCField1;
             *     20: required wstring PartCField2;
             * };
             */
            var myGenericObjectPropertyEventBlob = new SampleIfx.MyGenericObjectPropertyEvent<bool>
            {
                PartCField1 = "I'm a property",
                PartCField2 = "so am I"
            };

            IfxObject.LogExtendedPropertyChange<bool, SampleIfx.MyGenericObjectPropertyEvent<bool>>(
                "Book",                                   // The class name of the object instance.
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "Avaible In Stock",                       // The name of logged property.
                false,                                    // The property value to be logged.
                myGenericObjectPropertyEventBlob);        // partc blob.

            /*
             * struct MyObjectTextPropertyEvent:Ifx.ObjectTextPropertySchema
             * {
             *     10: required wstring PartCField1;
             *     20: required wstring PartCField2;
             * };
             */
            var myObjectTextPropertyEventBlob = new SampleIfx.MyObjectTextPropertyEvent
            {
                PartCField1 = "I'm a property",
                PartCField2 = "so am I"
            };

            IfxObject.LogExtendedTextPropertyChange<SampleIfx.MyObjectTextPropertyEvent>(
                "Book",                                   // The class name of the object instance.
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "Author",                                 // The name of logged property.
                "Margaret Mitchell",                      // The property value to be logged.
                myObjectTextPropertyEventBlob);           // partc blob.

            /*
             * struct MyObjectReferenceEvent:Ifx.ObjectReferenceSchema
             * {
             *     10: required wstring PartCField1;
             *     20: required wstring PartCField2;
             * };
             */
            var myObjectReferenceEventBlob = new SampleIfx.MyObjectReferenceEvent
            {
                PartCField1 = "I'm a property",
                PartCField2 = "so am I"
            };

            IfxObject.LogExtendedReference<SampleIfx.MyObjectReferenceEvent>(
                "Container",                              // The class name of the object instance. 
                "03415548-0000-0000-0000-008cfa02975c",   // The unique identifier of the object instance.
                "Node",                                   // The class name of the target object instance of the reference.
                "C8A640DD-F25E-4CA1-9265-1AC2628D0190",   // The unique identifier of the target object instance of the reference.
                "Container to Node reference",            // The name of logged reference.
                true,                                     // The existence of the reference, true for begin, false for end.  
                myObjectReferenceEventBlob);              // partc blob.

        }

        public static void MdmSample()
        {
            ErrorContext mdmError = new ErrorContext();

            // MeasureMetric usage sample
            MeasureMetric1D testMeasure = MeasureMetric1D.Create(
                    "Fabricator",                                        // MonitoringAccount
                    "Microsoft/Azure/Fabric/Tenant Manager/Management",  // MetricNamespace
                    "HI node count",                                     // MetricName
                    "Cluster",                                           // dimension 1
                    ref mdmError);
            if (testMeasure == null)
            {
                Console.WriteLine("Fail to create MeasureMetric, error code is {0:X}", mdmError.ErrorCode);
                Console.WriteLine("    error message: {0}", mdmError.ErrorMessage);
            }

            if (!testMeasure.LogValue(29, "Ch3PrdDDC03", ref mdmError))
            {
                Console.WriteLine("Fail to set MeasureMetric value, error code is {0:X}", mdmError.ErrorCode);
                Console.WriteLine("    error message: {0}", mdmError.ErrorMessage);
            }

            if (!testMeasure.LogValue(DateTime.UtcNow, 1, "HK2PrdApp03", ref mdmError))
            {
                Console.WriteLine("Fail to set MeasureMetric value, error code is {0:X}", mdmError.ErrorCode);
                Console.WriteLine("    error message: {0}", mdmError.ErrorMessage);
            }

            Thread.Sleep(1000);

            if (!testMeasure.LogValue(DateTime.UtcNow, 3, "HK2PrdApp03", ref mdmError))
            {
                Console.WriteLine("Fail to set MeasureMetric value, error code is {0:X}", mdmError.ErrorCode);
                Console.WriteLine("    error message: {0}", mdmError.ErrorMessage);
            }
        }

        public static void HealthSample()
        {
            MetadataCollection resourceIdentityDimensions = new MetadataCollection();
            MetadataCollection resourceMetadataCollection = new MetadataCollection();
            MetadataCollection watchdogMetadataCollection = new MetadataCollection();

            resourceIdentityDimensions.Add("Name", "ABC");

            bool result = IfxHealth.LogWatchdogHealthReport(
                "MonitoringAccount",
                "WatchdogName",
                "ResourceType",
                resourceIdentityDimensions,
                ResourceHealthStatus.Error,
                true,
                DateTime.UtcNow,
                "Message",
                "ArmResourceId",
                "IncarnationId",
                resourceMetadataCollection,
                watchdogMetadataCollection
                );

            if (result)
            {
                Console.WriteLine("Successfully called IfxHealth.LogWatchdogHealthReport.");
            }
            else
            {
                Console.WriteLine("IfxHealth.LogWatchdogHealthReport call failed.");
            }

            MetadataCollection annotationMetadataCollection = new MetadataCollection();

            result = IfxHealth.LogAnnotationHealthReport(
                "MonitoringAccount",
                "Content",
                "WatchdogName",
                "ResourceType",
                resourceIdentityDimensions,
                true,
                DateTime.UtcNow,
                "DisplayName",
                "ArmResourceId",
                annotationMetadataCollection
                );

            if (result)
            {
                Console.WriteLine("Successfully called IfxHealth.LogAnnotationHealthReport.");
            }
            else
            {
                Console.WriteLine("IfxHealth.LogAnnotationHealthReport call failed.");
            }
        }

        /// <summary>
        /// This sample illustrates how to use the Ifx tracing APIs.
        /// </summary>
        public static void TracingSample()
        {
            /*
             * Tracing a log message.
             */
            IfxTracer.LogMessage(
                IfxTracingLevel.Critical, // The trace level of this trace message.
                "C8A640DD-F25E-4CA1-9265-1AC2628D0190", // Tag Id: The unique identification of the instrumentation point in the source code.
                "A critical message."); // The message being logged.

            /*
             * Logging property bag.
             */
            IfxTracer.LogPropertyBag(
                IfxTracingLevel.Critical, // The trace level of this trace message.
                "343A9FA7-B2D5-43FD-8D27-99AE108FA988", // Tag Id: The unique identification of the instrumentation point in the source code.
                "Pet", // Property 1
                "Cat", // Value 1
                "Sound", // Property 2
                "Meow, meow"); // Value 2
        }

        /// <summary>
        /// This sample illustrates the usage of the Operation API to model different kinds
        /// of operations.
        /// </summary>
        public static void OperationsSample()
        {
            /*
             * Instrumenting an internal call.
             */

            // The default operation type of a newly created operation is Internal call.
            using (Operation operation = new Operation("Internal Call"))
            {
                // Set result for the operation.
                operation.SetResult(OperationResult.Success);
            }

            /*
             * Instrumenting a service Api call.
             */
            using (Operation operation = new Operation("Service API"))
            {
                // Set the operation type to Service Api.
                operation.ApiType = OperationApiType.ServiceApi;

                // Set result for the operation.
                operation.SetResult(OperationResult.Success);
            }

            /*
             * Instrumenting a client proxy call.
             */
            using (Operation operation = new Operation("Client Proxy"))
            {
                // Setting the Target endpoint for an operation automatically sets its type
                // to be ClientProxy.
                operation.TargetEndpointAddress = "http://bing";

                // Set result for the operation.
                operation.SetResult(OperationResult.Success);
            }

            /*
             * Instrumenting an internal call and designating marking it for QoS analysis.
             */
            using (Operation operation = new Operation("Internal Call"))
            {
                // Designate this operation to be considered for QoS analysis.
                operation.MarkImpactsQoS("Caller identity type", "Caller identity");

                // Set result for the operation.
                operation.SetResult(OperationResult.Success);
            }

            /*
             * Instrumenting a service Api call and designating it for QoS analysis.
             */
            using (Operation operation = new Operation("Service API"))
            {
                // Set the operation type to Service Api.
                operation.ApiType = OperationApiType.ServiceApi;

                // Designate this operation to be considered for QoS analysis.
                operation.MarkImpactsQoS("Caller identity type", "Caller identity");

                // Set result for the operation.
                operation.SetResult(OperationResult.Success);
            }


            /*
             * Instrumenting a client proxy call and designating it for QoS analysis.
             */
            using (Operation operation = new Operation("Client Proxy"))
            {
                // Setting the Target endpoint for an operation automatically sets its type
                // to be ClientProxy.
                operation.TargetEndpointAddress = "http://bing";

                // Designate this operation to be considered for QoS analysis.
                operation.MarkImpactsQoS("Caller identity type", "Caller identity");

                // Set result for the operation.
                operation.SetResult(OperationResult.Success);
            }
        }

        /// <summary>
        /// This sample illustrates the usage of ExtendedOperation API to model different kinds
        /// of operations and log additional user defined fields over what Ifx logs.
        /// </summary>
        public static void ExtendedOperationsSample()
        {
            SampleIfx.OperationEventPartC myOperationEvent = new SampleIfx.OperationEventPartC
            {
                PartCField1 = "PartC Field 1",
                PartCField2 = "PartC Field 2"
            };

            /*
             * Instrumenting an internal call.
             */

            // The default operation type of a newly created operation is Internal call.
            using (ExtendedOperation<SampleIfx.OperationEventPartC> operation = new ExtendedOperation<SampleIfx.OperationEventPartC>("Internal Call"))
            {
                // Set PartC.
                operation.PartC = myOperationEvent;

                // Set result for the operation.
                operation.SetResult(OperationResult.Success);
            }

            /*
             * Instrumenting a service Api call.
             */
            using (ExtendedOperation<SampleIfx.OperationEventPartC> operation = new ExtendedOperation<SampleIfx.OperationEventPartC>("Service API"))
            {
                // Set PartC.
                operation.PartC = myOperationEvent;

                // Set the operation type to Service Api.
                operation.ApiType = OperationApiType.ServiceApi;

                // Set result for the operation.
                operation.SetResult(OperationResult.Success);
            }

            /*
             * Instrumenting a client proxy call.
             */
            using (ExtendedOperation<SampleIfx.OperationEventPartC> operation = new ExtendedOperation<SampleIfx.OperationEventPartC>("Client Proxy"))
            {
                // Set PartC.
                operation.PartC = myOperationEvent;

                // Setting the Target endpoint for an operation automatically sets its type
                // to be ClientProxy.
                operation.TargetEndpointAddress = "http://bing";

                // Set result for the operation.
                operation.SetResult(OperationResult.Success);
            }

            SampleIfx.QoSEventPartC myQoSEvent = new SampleIfx.QoSEventPartC
            {
                PartCField1 = "PartC Field 1",
                PartCField2 = "PartC Field 2"
            };

            /*
             * Instrumenting an internal call and designating marking it for QoS analysis.
             */
            using (ExtendedOperation<SampleIfx.QoSEventPartC> qosOperation = new ExtendedOperation<SampleIfx.QoSEventPartC>("Internal Call"))
            {
                // Set PartC.
                qosOperation.PartC = myQoSEvent;

                // Designate this operation to be considered for QoS analysis.
                qosOperation.MarkImpactsQoS("Caller identity type", "Caller identity");

                // Set result for the operation.
                qosOperation.SetResult(OperationResult.Success);
            }

            /*
             * Instrumenting a service Api call and designating it for QoS analysis.
             */
            using (ExtendedOperation<SampleIfx.QoSEventPartC> qosOperation = new ExtendedOperation<SampleIfx.QoSEventPartC>("Service API"))
            {
                // Set PartC.
                qosOperation.PartC = myQoSEvent;

                // Set the operation type to Service Api.
                qosOperation.ApiType = OperationApiType.ServiceApi;

                // Designate this operation to be considered for QoS analysis.
                qosOperation.MarkImpactsQoS("Caller identity type", "Caller identity");

                // Set result for the operation.
                qosOperation.SetResult(OperationResult.Success);
            }

            /*
             * Instrumenting a client proxy call and designating it for QoS analysis.
             */
            using (ExtendedOperation<SampleIfx.QoSEventPartC> qosOperation = new ExtendedOperation<SampleIfx.QoSEventPartC>("Client Proxy"))
            {
                // Set PartC.
                qosOperation.PartC = myQoSEvent;

                // Setting the Target endpoint for an operation automatically sets its type
                // to be ClientProxy.
                qosOperation.TargetEndpointAddress = "http://bing";

                // Designate this operation to be considered for QoS analysis.
                qosOperation.MarkImpactsQoS("Caller identity type", "Caller identity");

                // Set result for the operation.
                qosOperation.SetResult(OperationResult.Success);
            }
        }

        /// <summary>
        /// This sample illustrates how to emit an event that is derived from Ifx PartA.
        /// </summary>
        public static void PartADerivedEventSample()
        {
            // MyEvent is defined in PartC.bond.
            SampleIfx.MyEvent myEvent = new SampleIfx.MyEvent
            {
                PartCField1 = "PartC Field 1",
                PartCField2 = "PartC Field 2"
            };

            IfxEvent.Log(myEvent);
        }

        public static void EmitLogs()
        {
            //IfxEvent.Log();
            IfxTracer.LogMessage(
            IfxTracingLevel.Critical, // The trace level of this trace message.
            "ComponentFoo", // Tag Id: This parameter can be used for identifying and grouping the source of the instrumentation point. e.g. at component, class or  file level.
                            // You can potentially use the value of this value to retrieve message emitted from particular parts of the source code in log search.
            "A critical message."); // The message being logged. 
        }

        public static void EmitMetrics()
        {
            ErrorContext mdmError = new ErrorContext();

            MeasureMetric1D testMeasure = MeasureMetric1D.Create(
                "unifiedtestmetrics",
                "unifiedtestmetrics",
                "MyMetricName",
                "MyDimensionName",
                ref mdmError);

            if (testMeasure == null)
            {
                Console.WriteLine("Fail to create MeasureMetric, error code is {0:X}, error message is {1}",
                    mdmError.ErrorCode,
                    mdmError.ErrorMessage);
            }
            else if (!testMeasure.LogValue(101, "MyDimensionValue", ref mdmError))
            {
                Console.WriteLine("Fail to log MeasureMetric value, error code is {0:X}, error message is {1}",
                    mdmError.ErrorCode,
                    mdmError.ErrorMessage);
            }
        }

        public static void EmitOperations()
        {
            using (Operation operation = new Operation("Some Operation"))
            {
                operation.SetResult(OperationResult.Success);
            }
        }
    }
}
