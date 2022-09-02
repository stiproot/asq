//using System;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;
//using processes.Models;
//using System.Collections.Generic;

//namespace processes.Engine
//{
    //public static class ActionFactory
    //{
        //public static async Task CombineTasks<TOut, TSIn, TFIn, TEIn>(
            //Func<Task<TOut>> task, 
            //Func<TSIn, Task> successHandlerFunc, 
            //Func<TFIn, Task> failureHandlerFunc, 
            //Func<TEIn, Task> exceptionHandlerFunc) 
                //where TOut : ProcessResult
                //where TEIn : Exception
        //{
            //object taskOutput = null;

            //try
            //{
                //taskOutput = await task();
            //}
            //catch(TEIn ex)
            //{
                //if(exceptionHandlerFunc != null)
                //{
                    //await exceptionHandlerFunc(ex);
                //}
                //else throw ex;
            //}
            //finally
            //{
                //if(taskOutput == null || ((ProcessResult)taskOutput).Failed)
                //{
                    //if(failureHandlerFunc != null)
                    //{
                        //await failureHandlerFunc((TFIn)taskOutput);
                    //}
                //}
                //else
                //{
                    //if(successHandlerFunc != null)
                    //{
                        //await successHandlerFunc((TSIn)taskOutput);
                    //}
                //}
            //}
        //}

        //public static async Task TaskRunner(IEnumerable<Task> tasks, Task successHandler, Task failureHandler, ILogger logger = null)
        //{
            //var allTasks = Task.WhenAll(tasks);

            //try
            //{
                //await allTasks;
            //}
            //catch(Exception ex)
            //{
                //throw;
                ////logger.LogError(ex, "TaskFuncFactoryLogic");
            //}
            //finally
            //{
                //switch(allTasks.Status)
                //{
                    //case TaskStatus.RanToCompletion:
                        //if(successHandler != null)
                        //{
                            //await successHandler;
                        //}
                        //break;

                    //case TaskStatus.Faulted:
                        //if(failureHandler != null)
                        //{
                            //await failureHandler;
                        //}
                        //break;

                    //default:
                        //break;
                //}
            //}
        //}

        //public static Func<TTParam1, TTParam2, TTParam3, Task<IEnginePacket>> CombineTasksBeta<TTParam1, TTParam2, TTParam3>(
            //Func<TTParam1, TTParam2, TTParam3, Task<IEnginePacket>> t, 
            //Func<IEnginePacket, Task> handleSucessFact, 
            //Func<Exception, Task> handleExceptionFact) 
        //{
            //return async (TTParam1 taskParam1, TTParam2 taskParam2, TTParam3 taskParam3) =>
            //{
                //IEnginePacket taskOutput = null;

                //try
                //{
                    //taskOutput = await t(taskParam1, taskParam2, taskParam3);
                //}
                //catch(Exception ex)
                //{
                    //if(handleExceptionFact != null)
                    //{
                        //await handleExceptionFact(ex);
                    //}
                    //throw ex;
                //}
                //finally
                //{
                    //if(handleSucessFact != null)
                    //{
                        //await handleSucessFact(taskOutput);
                    //}
                //}

                //return taskOutput;
            //};
        //}
    //}
//}