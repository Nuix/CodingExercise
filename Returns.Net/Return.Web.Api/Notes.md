# Notes

-Took about 130m to complete since the prompt specified a couple of hours

Endpoint post over get - querying usually you want to pass in a body for some complex query system. For this, it needs
to be post as get does not accept a body.

Logging - using built in ILogger but so can use serilog or nlog if wanting to. For now, will log to console
but would prefer to log to some type of database. should use a generic interceptor for all endpoints and log there

Exception handling - should wrap results to include api status codes and exceptions. Theres a difference between 
breaking and no responses, so if valid always return a result, if not it should return bad status code.

Other
-Interfaced out share service so other public apis can be used, say one is cheaper, or even start tracking internally
-tend to use IReadOnlyX where possible for read only operations (and records for responses for immutability).
-Models is netstandard 2.0 so any older project that consumes the api can use the model structures. just needs a helper class for the lang version diff
-wouldve used minimal api setup, but controller works fine also
-wouldve liked to add more proper responses and wrap the return in a result object IE Result<T> which would also contain optional error obj'
