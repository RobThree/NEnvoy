[[Source](https://gathering.tweakers.net/forum/list_message/67668598#67668598)]

If you're aware of other URL's then please let me know; either [make an issue](https://github.com/RobThree/NEnvoy/issues/new?title=New%20Envoy%20url) or send a pull request on this document (or even better: with the implementation :wink:).

Status | Url | Note
-|-|-
❕|https://envoy/home.json
✅|https://envoy/info.xml
✅|https://envoy/auth/check_jwt
❌|https://envoy/admin/home?locale=en
✅|https://envoy/admin/lib/wireless_display.json?site_info=0
✅|https://envoy/api/v1/production
❕|https://envoy/api/v1/production/inverters | Throws (401 unauthorized) for some reason
❌|https://envoy/event?locale=en
❌|https://envoy/event.json
❌|https://envoy/installer/setup/home
❌|https://envoy/inventory?locale=en | Locale doesn't seem to make a difference (tried `de`, `nl`, `it` and some others)
❌|https://envoy/inventory.json
❌|https://envoy/inventory.json?deleted=1 | Can someone provide an example json and point out the difference with `https://envoy/inventory.json` above?
❌|https://envoy/ivp/ensemble/inventory
❌|https://envoy/ivp/livedata/status
✅|https://envoy/ivp/meters
✅|https://envoy/ivp/meters/readings
❕|https://envoy/ivp/meters/reports/consumption | Some properties on `ConsumptionValues` are still unclear.
❌|https://envoy/ivp/peb/newscan
❌|https://envoy/production.json | Won't be implemented, see `https://envoy/production.json?details=1` below
❌|https://envoy/production.json?details=1
❌|https://envoy/prov
❌|https://envoy/stream/meter | SSE

# Legend:

Symbol | Meaning
-|-
✅|Implemented
❌|Not implemented
❕|Partially implemented