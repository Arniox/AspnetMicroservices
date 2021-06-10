foreach ($CipherSuite in $(Get-TlsCipherSuite).Name) {
    if ($CipherSuite.substring(0, 7) -eq "TLS_DHE") {
        "Disabling cipher suite: " + $CipherSuite
        Disable-TlsCipherSuite -Name $CipherSuite
    }
    else {
        "Existing enabled cipher suite will remain enabled: " + $CipherSuite
    }
}

foreach ($CipherSuite in $(Get-TlsCipherSuite).Name) {
    if ($CipherSuite.substring(0, 7) -eq "TLS_DHE") {
        $CipherSuite
    }
}

# TLS_DHE_* ciphers can be disabled by using Group Policy. Refer to Prioritizing Schannel Cipher Suites to configure the "SSL Cipher Suite Order" group policy.

# Policy URL: Computer Configuration -> Administrative Templates -> Network -> SSL Configuration Settings
# Policy Setting: SSL Cipher Suite Order setting.
