﻿!function (t) { var e = {}; function n(a) { if (e[a]) return e[a].exports; var r = e[a] = { i: a, l: !1, exports: {} }; return t[a].call(r.exports, r, r.exports, n), r.l = !0, r.exports } n.m = t, n.c = e, n.d = function (t, e, a) { n.o(t, e) || Object.defineProperty(t, e, { enumerable: !0, get: a }) }, n.r = function (t) { "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(t, Symbol.toStringTag, { value: "Module" }), Object.defineProperty(t, "__esModule", { value: !0 }) }, n.t = function (t, e) { if (1 & e && (t = n(t)), 8 & e) return t; if (4 & e && "object" == typeof t && t && t.__esModule) return t; var a = Object.create(null); if (n.r(a), Object.defineProperty(a, "default", { enumerable: !0, value: t }), 2 & e && "string" != typeof t) for (var r in t) n.d(a, r, function (e) { return t[e] }.bind(null, r)); return a }, n.n = function (t) { var e = t && t.__esModule ? function () { return t.default } : function () { return t }; return n.d(e, "a", e), e }, n.o = function (t, e) { return Object.prototype.hasOwnProperty.call(t, e) }, n.p = "", n(n.s = 41) }({ 41: function (t, e) { gapi.analytics.ready(function () { var t = /(\d+)daysAgo/, e = /\d{4}-\d{2}-\d{2}/; function n(n) { if (e.test(n)) return n; var r, i, o, s = t.exec(n); if (s) return a(+s[1]); if ("today" == n) return a(0); if ("yesterday" == n) return a(1); if ("firstDayOfMonth" == n) return r = new Date, i = r.getFullYear().toString().padStart(4, 0), o = (r.getMonth() + 1).toString().padStart(2, 0), "".concat(i, "-").concat(o, "-01"); throw new Error("Cannot convert date " + n) } function a(t) { var e = new Date; e.setDate(e.getDate() - t); var n = String(e.getMonth() + 1); n = 1 == n.length ? "0" + n : n; var a = String(e.getDate()); return a = 1 == a.length ? "0" + a : a, e.getFullYear() + "-" + n + "-" + a } gapi.analytics.createComponent("DateRangeSelector", { execute: function () { var t = this.get(); t["start-date"] = t["start-date"] || "7daysAgo", t["end-date"] = t["end-date"] || "yesterday", this.container = "string" == typeof t.container ? document.getElementById(t.container) : t.container, t.template && (this.template = t.template), this.container.innerHTML = this.template; var e = this.container.querySelectorAll("input"); return this.startDateInput = e[0], this.startDateInput.value = n(t["start-date"]), this.endDateInput = e[1], this.endDateInput.value = n(t["end-date"]), this.setValues(), this.setMinMax(), this.container.onchange = this.onChange.bind(this), this }, onChange: function () { this.setValues(), this.setMinMax(), this.emit("change", { "start-date": this["start-date"], "end-date": this["end-date"] }) }, setValues: function () { this["start-date"] = this.startDateInput.value, this["end-date"] = this.endDateInput.value }, setMinMax: function () { this.startDateInput.max = this.endDateInput.value, this.endDateInput.min = this.startDateInput.value }, template: '<div class="DateRangeSelector">  <div class="DateRangeSelector-item">    <label>Start Date</label>     <input type="date">  </div>  <div class="DateRangeSelector-item">    <label>End Date</label>     <input type="date">  </div></div>' }) }) } });
//# sourceMappingURL=date-range-selector.js.map