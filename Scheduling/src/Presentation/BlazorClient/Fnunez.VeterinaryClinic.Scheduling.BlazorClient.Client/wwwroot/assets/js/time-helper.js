// Returns a date as a string value in ISO format.
function getLocalTime() {
    return new Date().toISOString();
}

// Returns difference in minutes between the time on the local computer and Universal Coordinated Time (UTC).
function getTimezoneOffset() {
    return new Date().getTimezoneOffset();
}

// Returns a date converted to a string using Universal Coordinated Time (UTC).
function getUtcTime() {
    return new Date().toUTCString();
}