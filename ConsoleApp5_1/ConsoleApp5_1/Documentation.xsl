<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes" encoding="UTF-8"/>
  
  <xsl:template match="/">
    <html>
      <head>
        <title>Пиццерия - Документация API</title>
        <style>
          body { font-family: 'Segoe UI', Arial, sans-serif; margin: 20px; background: #f5f5f5; }
          h1 { color: #2c3e50; border-bottom: 3px solid #e74c3c; padding-bottom: 10px; }
          h2 { color: #34495e; margin-top: 30px; background: #ecf0f1; padding: 8px 15px; border-radius: 5px; cursor: pointer; }
          h2:hover { background: #d5dbdb; }
          h3 { color: #2980b9; margin-top: 20px; margin-bottom: 5px; }
          .class { background: white; padding: 15px; margin: 15px 0; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
          .method { margin-left: 20px; padding: 10px; border-left: 3px solid #3498db; margin-top: 10px; background: #fafafa; border-radius: 0 5px 5px 0; }
          .property { margin-left: 20px; padding: 8px; border-left: 3px solid #27ae60; margin-top: 5px; background: #f0fff0; border-radius: 0 5px 5px 0; }
          .summary { color: #2c3e50; margin: 10px 0; }
          .parameters { background: #f8f9fa; padding: 10px; margin: 10px 0; border-radius: 5px; }
          .param { margin: 5px 0; font-family: monospace; }
          .param-name { color: #e74c3c; font-weight: bold; }
          .returns { color: #27ae60; margin-top: 10px; }
          .exception { color: #c0392b; margin-top: 10px; }
          .exception-name { font-weight: bold; }
          .assembly { background: #2c3e50; color: white; padding: 5px 15px; border-radius: 5px; display: inline-block; margin-bottom: 20px; }
          hr { margin: 30px 0; }
          .toggle-btn { float: right; font-size: 14px; background: #3498db; color: white; border: none; border-radius: 3px; padding: 2px 8px; cursor: pointer; }
        </style>
        <script>
          function toggleMembers(id) {
            var elem = document.getElementById(id);
            if (elem.style.display === 'none') {
              elem.style.display = 'block';
            } else {
              elem.style.display = 'none';
            }
          }
        </script>
      </head>
      <body>
        <h1>🍕 Пиццерия - Документация API</h1>
        <div class="assembly">
          <strong>Сборка:</strong> <xsl:value-of select="doc/assembly/name"/>
        </div>
        <p>Сгенерировано из XML-комментариев</p>
        
        <xsl:apply-templates select="doc/members"/>
      </body>
    </html>
  </xsl:template>
  
  <xsl:template match="members">
    <xsl:for-each select="member[contains(@name, 'T:')]">
      <xsl:variable name="typeName" select="substring-after(@name, 'T:')"/>
      
      <div class="class">
        <h2>
          📦 <xsl:value-of select="$typeName"/>
          <button class="toggle-btn" onclick="toggleMembers('members-{$typeName}')">▼</button>
        </h2>
        <div class="summary"><xsl:value-of select="summary"/></div>
        
        <div id="members-{$typeName}">
          <xsl:for-each select="//member[starts-with(@name, concat('M:', $typeName, '.#ctor'))]">
            <xsl:call-template name="methodTemplate">
              <xsl:with-param name="methodInfo" select="."/>
              <xsl:with-param name="methodName" select="'#ctor'"/>
            </xsl:call-template>
          </xsl:for-each>
          
          <xsl:for-each select="//member[starts-with(@name, concat('P:', $typeName, '.'))]">
            <xsl:call-template name="propertyTemplate">
              <xsl:with-param name="prop" select="."/>
              <xsl:with-param name="propName" select="substring-after(@name, concat('P:', $typeName, '.'))"/>
            </xsl:call-template>
          </xsl:for-each>
          
          <xsl:for-each select="//member[starts-with(@name, concat('M:', $typeName, '.')) and not(contains(@name, '.#ctor'))]">
            <xsl:call-template name="methodTemplate">
              <xsl:with-param name="methodInfo" select="."/>
              <xsl:with-param name="methodName" select="substring-after(@name, concat('M:', $typeName, '.'))"/>
            </xsl:call-template>
          </xsl:for-each>
        </div>
      </div>
    </xsl:for-each>
  </xsl:template>
  
  <xsl:template name="propertyTemplate">
    <xsl:param name="prop"/>
    <xsl:param name="propName"/>
    
    <div class="property">
      <h3>🔷 <xsl:value-of select="$propName"/></h3>
      <div class="summary"><xsl:value-of select="$prop/summary"/></div>
    </div>
  </xsl:template>
  
  <xsl:template name="methodTemplate">
    <xsl:param name="methodInfo"/>
    <xsl:param name="methodName"/>
    
    <div class="method">
      <h3>
        <xsl:choose>
          <xsl:when test="$methodName = '#ctor'">🏗️ Конструктор</xsl:when>
          <xsl:otherwise>⚙️ <xsl:value-of select="$methodName"/></xsl:otherwise>
        </xsl:choose>
      </h3>
      <div class="summary"><xsl:value-of select="$methodInfo/summary"/></div>
      
      <xsl:if test="$methodInfo/param">
        <div class="parameters">
          <strong>📋 Параметры:</strong>
          <xsl:for-each select="$methodInfo/param">
            <div class="param">
              <span class="param-name"><xsl:value-of select="@name"/></span> - <xsl:value-of select="."/>
            </div>
          </xsl:for-each>
        </div>
      </xsl:if>
      
      <xsl:if test="$methodInfo/returns">
        <div class="returns">
          <strong>↩️ Возвращает:</strong> <xsl:value-of select="$methodInfo/returns"/>
        </div>
      </xsl:if>
      
      <xsl:if test="$methodInfo/exception">
        <div class="exception">
          <strong>⚠️ Исключения:</strong>
          <div class="exception-name"><xsl:value-of select="$methodInfo/exception/@cref"/></div>
          <div><xsl:value-of select="$methodInfo/exception"/></div>
        </div>
      </xsl:if>
    </div>
  </xsl:template>
  
</xsl:stylesheet>