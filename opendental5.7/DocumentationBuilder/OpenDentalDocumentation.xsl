<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/">
  <html>
  <body>
    <p>
      Open Dental Database Documentation<br/>
      Version 
      <xsl:value-of select="database/@version"/>
    </p>
    <p>
      <table border="1" cellpadding="1" cellspacing="0" bgcolor="#F1F4F8">
        <tr bgcolor="#A5B7C9">
          <td><b>Database Tables</b></td>
        </tr>
        <xsl:for-each select="database/table">
        <tr>
          <td>
            <a>
              <xsl:attribute name="href">
                #<xsl:value-of select="@name"/>
              </xsl:attribute>
              <xsl:value-of select="@name"/>
            </a>
          </td>
        </tr>
        </xsl:for-each>
      </table>
    </p>
    <xsl:for-each select="database/table">
      <p>
      <a>
        <xsl:attribute name="name">
          <xsl:value-of select="@name"/>
        </xsl:attribute>
      </a>
      <table width="650" border="1" cellpadding="1" cellspacing="0" bgcolor="#F1F4F8">
        <tr bgcolor="#A5B7C9">
          <td><b><xsl:value-of select="@name"/></b></td>
        </tr>
        <tr>
          <td><xsl:value-of select="summary"/></td>
        </tr>
      </table>
      <table width="650" border="1" cellpadding="1" cellspacing="0" bgcolor="#F1F4F8">
        <tr bgcolor="#D0D1D2">
          <td width="50">Order</td>
          <td width="100">Name</td>
          <td width="100">Type</td>
          <td width="400">Summary</td>
        </tr>
        <xsl:for-each select="column">
        <tr>
          <td width="50"><xsl:value-of select="@order"/></td>
          <td width="100"><xsl:value-of select="@name"/></td>
          <td width="100"><xsl:value-of select="@type"/></td>
          <td width="400">
            <xsl:choose>
              <xsl:when test="@fk">
                FK to 
                <a>
                  <xsl:attribute name="href">
                    #<xsl:value-of select="@fk"/>
                  </xsl:attribute>
                  <xsl:value-of select="@fk"/>
                </a>
                <xsl:value-of select="substring(summary,7 + string-length(@fk))"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="summary"/>
              </xsl:otherwise>
            </xsl:choose>
          </td>
        </tr>
          <xsl:for-each select="Enumeration">
            <xsl:for-each select="EnumValue">
            <tr>
              <td width="50"></td>
              <td width="100"></td>
              <td width="100"></td>
              <td width="400">
                <xsl:value-of select="@name"/>: <xsl:value-of select="."/>
              </td>
            </tr>
            </xsl:for-each>
          </xsl:for-each>
        </xsl:for-each>
      </table>
      </p>
    </xsl:for-each>
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>








