[[Source 1](https://gathering.tweakers.net/forum/list_message/67668598#67668598)], [[Source2](Docs/ENVOY-Envoy%20REST%20Interface.pdf)]

If you're aware of other URL's then please let me know; either [make an issue](https://github.com/RobThree/NEnvoy/issues/new?title=New%20Envoy%20url) or send a pull request on this document (or even better: with the implementation :wink:).

**NOTE:** Not all methods may be implemented; some may not work or not exist (anymore).

Status | Url | Note
-|-|-
❕|https://envoy/home.json
✅|https://envoy/info.xml
✅|https://envoy/auth/check_jwt
❌|https://envoy/admin/home?locale=en
✅|https://envoy/admin/lib/wireless_display.json?site_info=0
❌|https://envoy/admin/lib/tariff
✅|https://envoy/api/v1/production
❕|https://envoy/api/v1/production/inverters | Throws (401 unauthorized) for some reason
❌|https://envoy/event.json
❌|https://envoy/installer/setup/home
❌|https://envoy/inventory?locale=en | Locale doesn't seem to make a difference (tried `de`, `nl`, `it` and some others)
❌|https://envoy/inventory.json
❌|https://envoy/inventory.json?deleted=1 | Can someone provide an example json and point out the difference with `https://envoy/inventory.json` above?
❌|https://envoy/ivp/adc/record?eid=1090519312
❌|https://envoy/ivp/adc/record?eid=1090519312&tpm=true
❌|https://envoy/ivp/arf/profile
❌|https://envoy/ivp/ens_rest/inverters_status
❌|https://envoy/ivp/ensemble/inventory
❌|https://envoy/ivp/ensemble/submod | To get list of all submod serial number.
❌|https://envoy/ivp/ensemble/dataraw/<Serial number of EnChg> | This will provide telemetry status of all units of encharge and submodules.
❌|https://envoy/ivp/ensemble/dry_contacts
❌|https://envoy/ivp/ensemble/relay
❌|https://envoy/ivp/ensemble/status
❌|https://envoy/ivp/livedata/status
❌|https://envoy/ivp/livedata/status
❌|https://envoy/ivp/livedata/status/counters
✅|https://envoy/ivp/meters
✅|https://envoy/ivp/meters/readings
❕|https://envoy/ivp/meters/reports/consumption | Some properties on `ConsumptionValues` are still unclear.
❌|https://envoy/ivp/peb/newscan
❌|https://envoy/ivp/sc/status
❌|https://envoy/ivp/sc/pvlimit
❌|https://envoy/ivp/ss/pcs_settings
❌|https://envoy/ivp/ss/pel_settings
❌|https://envoy/ivp/ss/der_settings
❌|https://envoy/ivp/ss/dry_contact_settings
❌|https://envoy/ivp/ss/gen_config
❌|https://envoy/ivp/ss/gen_schedule
❌|https://envoy/ivp/peb/rpttemp
❌|https://envoy/ivp/zb/status
❌|https://envoy/ivp/zb/commission
❌|https://envoy/ivp/zb/pairing_status
❌|https://envoy/ivp/grest/profile/acti
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