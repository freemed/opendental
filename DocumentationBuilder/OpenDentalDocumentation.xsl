<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<xsl:template match="/">
  <html>
  <body>
    <xsl:for-each select="database/table">
      <p><b><xsl:value-of select="@name"/></b>
      <br><xsl:value-of select="summary"/></br></p>
      <table border="2">
        <tr>
          <td>Order</td>
          <td>Name</td>
          <td>Type</td>
          <td>Summary</td>
        </tr>
        <xsl:for-each select="column">
        <tr>
          <td><xsl:value-of select="@order"/></td>
          <td><xsl:value-of select="@name"/></td>
          <td><xsl:value-of select="@type"/></td>
          <td><xsl:value-of select="summary"/></td>
        </tr>
        </xsl:for-each>
      </table>
    </xsl:for-each>
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>