# DOCUMENTACIÓN DEL SISTEMA - BOTANAS

## 📊 Sistema de Gestión para Distribuidora de Botanas

Este es un **sistema ERP (Enterprise Resource Planning)** diseñado específicamente para una empresa distribuidora de botanas (snacks) en México, desarrollado por **TIIM Tecnología**.

---

## 🏗️ Arquitectura del Sistema

El proyecto sigue una **arquitectura en capas** (.NET Framework 4.8, C# 7.3):

### Capas del Sistema

1. **BotanasAzure (Capa de Presentación)**
   - Interfaz de usuario con Windows Forms
   - Pantallas de ventas, inventarios, clientes, facturación
   - Componente: `Botanas.csproj`

2. **AiresNegocio (Capa de Lógica de Negocio)**
   - Reglas de negocio
   - Validaciones
   - Cálculos de impuestos y totales
   - Componente: `BotanasNegocio.csproj`

3. **AiresDatos (Capa de Acceso a Datos)**
   - Conexión con SQL Server
   - Operaciones CRUD
   - Stored Procedures
   - Componente: `BotanasDatos.csproj`

4. **AiresEntidades (Capa de Entidades)**
   - Modelos de datos
   - DTOs (Data Transfer Objects)
   - Entidades: Cliente, Producto, Pedido, Factura, etc.
   - Componente: `BotanasEntidades.csproj`

5. **AiresUtilerias (Capa de Utilidades)**
   - Herramientas auxiliares
   - Facturación electrónica
   - Utilidades de conversión
   - Componente: `BotanasUtilerias.csproj`

6. **WSConecFM (Web Services)**
   - Integración con servicios de facturación
   - Consumo de Web Services SOAP
   - Componente: `WSConecFM.csproj`

---

## 🎯 Funcionalidades Principales

### 1. Gestión de Ventas
- ✅ Ventas al mayoreo
- ✅ Ventas al menudeo
- ✅ Punto de venta (POS)
- ✅ Pre-ventas
- ✅ Registro de pedidos
- ✅ Control de crédito a clientes
- ✅ Múltiples listas de precios (hasta 5 precios especiales)

### 2. Facturación Electrónica (CFDI 4.0)

#### Tipos de Comprobantes Soportados:
- **Facturas de Ingreso (CFDI 4.0)**
  - Método: `Facturar40()` - Versión estándar
  - Método: `Facturar40Recalculo()` - Con recálculo de importes

- **Complementos de Pago (Versión 2.0)**
  - Método: `FacturarComplementoPago40PADE()`
  - Método: `FacturarComplementoPago40PADERecalculo()`
  - Método: `FacturarComplementoPago33PADE()` - Compatibilidad con CFDI 3.3

- **Notas de Crédito**
  - Método: `FacturarNotaDeCredito40PADE()`
  - Devoluciones y descuentos
  - Relacionadas con facturas originales

- **Cancelación de CFDI**
  - Método: `CancelarPADE()`
  - Método: `Cancelar()` - Método alternativo

#### Características de Facturación:
- ✅ Integración con **PADE** (Proveedor Autorizado de Certificación)
- ✅ Generación automática de XML y PDF
- ✅ Codificación Base64 de archivos
- ✅ Manejo de impuestos: **IVA (16%)** e **IEPS (8%)**
- ✅ Soporte para productos gravados y exentos
- ✅ Tasa 0% configurable
- ✅ Observaciones personalizadas (pagaré automático)
- ✅ Timeout extendido (600 segundos)

#### Impuestos Manejados:
- **IVA**: 16% (Tasa estándar) / 0% (Productos exentos)
- **IEPS**: 8% (Impuesto Especial sobre Producción y Servicios)
- **ISR**: 10% (Retención, cuando aplica)
- **IVA Retenido**: Cuando aplica

### 3. Gestión de Inventarios
- ✅ Control de productos
- ✅ Catálogo de productos con código de barras
- ✅ Entradas de mercancía (productos e insumos)
- ✅ Salidas de mercancía
- ✅ Traspasos entre almacenes
- ✅ Control de existencias mínimas/máximas
- ✅ Productos con y sin IEPS
- ✅ Múltiples almacenes
- ✅ Costo y precios de venta

### 4. Gestión de Clientes
- ✅ Registro de clientes
- ✅ Datos fiscales (RFC, Régimen Fiscal, Uso CFDI)
- ✅ Cuentas por cobrar
- ✅ Estados de cuenta
- ✅ Control de crédito y cobranza
- ✅ Parcialidades de pago
- ✅ Seguimiento de saldos
- ✅ Cliente genérico: "PUBLICO EN GENERAL" (RFC: XAXX010101000)

### 5. Control de Acceso

Sistema con **múltiples roles de usuario**:

| ID | Rol | Permisos |
|----|-----|----------|
| 1 | Master | Acceso completo al sistema |
| 2 | Administrador de Insumos | Productos, entradas/salidas |
| 3 | Administrador de Producción | Control de producción |
| 4 | Administrador de Almacén | Inventarios y almacenes |
| 5 | Punto de Venta | Ventas y cobros |
| 6 | Cuentas por Cobrar | Cobranza y estados de cuenta |
| 7 | Administrador Punto de Venta | Gestión de ventas |
| 8 | Punto de Venta Menudeo | Ventas al detalle |
| 9 | Administrador de Inventarios | Control de stock |
| 10 | Cuentas por Cobrar Ventas | Cobranza de ventas |
| 11 | Gerente de Ventas | Supervisión de ventas |
| 12 | Supervisor | Supervisión general |
| 13 | Gerente de Almacén | Gestión de almacenes |
| 14 | Gerente de Producción | Gestión de producción |

---

## 💡 Características Especiales

### 1. Facturación Específica para Botanas
- **Productos con IEPS**: Impuesto especial a bebidas y alimentos con alto contenido calórico
- **Cálculo automático**: Separación de base gravable e IEPS
- **Múltiples precios**: Hasta 5 precios especiales configurables
- **Precio sin IVA**: Manejo de productos exentos

### 2. Sistema de Licenciamiento
- ✅ Control de vencimiento de licencia
- ✅ Validación de fechas de uso
- ✅ Bloqueo automático al vencer
- ✅ Notificaciones al usuario

### 3. Multi-empresa
- ✅ Soporte para múltiples empresas emisoras
- ✅ Configuración fiscal por empresa
- ✅ Régimen fiscal configurable
- ✅ Diferentes certificados (CSD)

### 4. Observaciones Personalizadas
- **En Facturas**: Incluye pagaré automático con términos y condiciones
- **En Complementos de Pago**: Detalle de facturas relacionadas con tabla formateada
- **En Notas de Crédito**: Descripción del descuento aplicado

### 5. Formato CSV para PADE
El sistema genera archivos CSV con estructura específica:
- `CFDI`: Datos del comprobante
- `EMIS`: Datos del emisor
- `RECE`: Datos del receptor
- `CONC`: Conceptos (productos/servicios)
- `CIMT`: Impuestos trasladados por concepto
- `IMPU`: Resumen de impuestos
- `IMPT`: Impuestos trasladados totales
- `CRPS20`: Complemento de pagos versión 2.0
- `CPAG20`: Pago
- `CPDR20`: Documento relacionado
- `CPTDR20`: Impuestos del documento relacionado

---

## 🔧 Tecnologías Utilizadas

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| .NET Framework | 4.8 | Framework base |
| C# | 7.3 | Lenguaje de programación |
| Windows Forms | - | Interfaz de usuario |
| SQL Server | - | Base de datos |
| Web Services SOAP | - | Integración PADE |
| Base64 Encoding | - | Codificación XML/PDF |

---

## 📝 Detalles del Archivo UtiFacturacionPruebas.cs

### Propósito
Clase de utilería para la generación de CFDIs en ambiente de **pruebas**. Contiene toda la lógica de construcción de comprobantes fiscales digitales.

### Credenciales de Prueba
```csharp
string UserID = "UsuarioPruebasWS";
string UserPass = "b9ec2afa3361a59af4b4d102d3f704eabdf097d4";
string Contrato = "ca2be778-3ba5-48f3-8fae-00b831faebea"; // TIIM
string Usuario = "pavel_tiim@hotmail.com";
string Contraseña = "tiimFac10!";
```

### Tasas de Impuestos
```csharp
public decimal IVA = 0.16m;  // 16% IVA
public decimal IEPS = 0.08m; // 8% IEPS
public decimal ISR = 0.10m;  // 10% ISR (retención)
```

### Métodos Principales

#### 1. Facturar40()
Genera factura CFDI 4.0 estándar.

**Parámetros**:
- `EntEmpresa Emisor`: Datos del emisor
- `EntPedido Pedido`: Pedido a facturar
- `List<EntProducto> ListaProductos`: Productos del pedido
- `EntCliente Cliente`: Datos del cliente
- Parámetros fiscales: Serie, Fecha, FormaPago, MetodoPago, etc.
- Parámetros de impuestos: IVA, IEPS, Retenciones

**Retorna**: UUID del comprobante timbrado

**Características**:
- Genera pagaré automático
- Maneja productos con y sin IEPS
- Guarda XML y PDF en disco

#### 2. Facturar40Recalculo()
Versión con recálculo automático de importes.

**Diferencias**:
- Recalcula subtotal e importes
- Usa decimales con mayor precisión
- Ajusta diferencias de redondeo

#### 3. FacturarComplementoPago40PADE()
Genera complemento de pago versión 2.0.

**Características**:
- Relaciona múltiples facturas
- Calcula impuestos proporcionales
- Maneja parcialidades
- Uso CFDI: CP01

#### 4. FacturarComplementoPago40PADERecalculo()
Complemento de pago con recálculo y formato detallado.

**Características adicionales**:
- Tabla formateada de facturas
- Detalles de cada pago
- Información resumida

#### 5. FacturarNotaDeCredito40PADE()
Genera nota de crédito (CFDI tipo Egreso).

**Características**:
- Tipo de comprobante: "E"
- Relación con factura original
- Tipos de relación soportados
- Uso CFDI: S01 para público general

#### 6. CancelarPADE()
Cancela un CFDI timbrado.

**Parámetros**:
- UUID a cancelar
- Motivo de cancelación
- UUID de sustitución (opcional)

### Proceso de Timbrado

```
1. Construcción del CSV
   ├─ Datos del CFDI (versión, serie, folio, fecha)
   ├─ Datos del Emisor
   ├─ Datos del Receptor
   ├─ Conceptos (productos/servicios)
   ├─ Impuestos por concepto
   └─ Totales de impuestos

2. Configuración de Opciones
   ├─ PLUGIN_PROCESO:GENERICO_CFDI40:1.0
   ├─ CALCULAR_SELLO
   ├─ ESTABLECER_NO_CERTIFICADO
   ├─ GENERAR_PDF
   ├─ CADENA_ORIGINAL (opcional)
   └─ OBSERVACIONES (Base64)

3. Invocación al Web Service PADE
   └─ wPade.procesarArchivo()

4. Procesamiento de Respuesta
   ├─ Extracción del UUID
   ├─ Decodificación XML (Base64)
   ├─ Decodificación PDF (Base64)
   └─ Guardado de archivos

5. Manejo de Errores
   └─ Exception con respuesta completa
```

---

## 📂 Estructura de Archivos Generados

### Nomenclatura
- **XML**: `{UUID}.xml`
- **PDF**: `{UUID}.pdf`

### Ubicación
Configurada en `PathGuardaArchivos` (parámetro del método)

Ejemplo:
```
C:\Facturas\2024\
├─ 12345678-1234-5678-1234-567812345678.xml
└─ 12345678-1234-5678-1234-567812345678.pdf
```

---

## 🔍 Casos de Uso Especiales

### 1. Público en General
```csharp
if (Cliente.RFC == "XAXX010101000")
{
    if(Cliente.NombreFiscal == "PUBLICO EN GENERAL")
        Cliente.NombreFiscal = "PUBLICO EN GENERAL.";
    Cliente.CP = Emisor.CP;
    UsoCFDI = "S01"; // Para notas de crédito
}
```

### 2. Productos con IEPS
```csharp
if (p.IncluyeIeps)
{
    porcentaje += (IEPS) * 100;
    cantidadIeps = Math.Round((Math.Round(p.Precio, 2) * (IEPS * 100)) / porcentaje, 4);
    importe = Math.Round(p.Precio, 2) - cantidadIeps;
}
```

### 3. Productos sin IVA (Exentos)
```csharp
else
{
    ivas = 0.00m;
    importe = Math.Round(Math.Round(p.Precio, 2), 4);
    incluyeSinIVA = true;
}
```

---

## 🚨 Manejo de Errores

### Errores en Timbrado
```csharp
try
{
    uuid = respuesta.Remove(0, respuesta.IndexOf("<UUID>")).Replace("<UUID>", "").Remove(36);
    // Procesamiento...
}
catch (Exception ex)
{
    throw new Exception("-ERROR EN TIMBRADO-\n" + respuesta + "\n\n" + textoCSV);
}
```

La excepción incluye:
1. Mensaje de error del PAC
2. CSV completo enviado (para debugging)

### Errores en Cancelación
```csharp
if (respuesta.Contains("false"))
{
    throw new Exception("-ERROR AL CANCELAR-\n" + respuesta + "\n\n" + arregloS);
}
```

---

## 📊 Entidades Principales

### EntEmpresa
```csharp
- RFC
- NombreFiscal
- RegimenFiscalId
- TasaOCuota (IVA)
- TipoFactor (Tasa/Exento/Cuota)
- CP (Código Postal)
- NumeroReferencia (Contrato PAC)
```

### EntCliente
```csharp
- RFC
- NombreFiscal
- RegimenFiscalId
- CP (Código Postal)
- Calle, NoExterior, NoInterior
- Colonia, Localidad, Municipio, Estado
```

### EntPedido
```csharp
- Factura (Folio)
- SubTotal
- Total
- IVA
- IEPS
- IVARetencion
- ISRRetencion
- Pago
- NotasCredito
- Debe (Saldo pendiente)
```

### EntProducto
```csharp
- Codigo
- ClaveProductoServicio (SAT)
- ClaveUnidad (SAT)
- Unidad
- Descripcion
- Cantidad
- PrecioVenta
- IncluyeIeps (bool)
```

### EntFactura
```csharp
- UUID
- SerieFactura
- NumeroFactura
- Total
- IVA
- IEPS
- Pago (monto del pago parcial)
- Saldo (saldo anterior)
- Parcialidad (número de parcialidad)
```

---

## 🔐 Seguridad y Configuración

### Ambiente de Producción
Para cambiar a producción, descomentar:
```csharp
//DESCOMENTAR EN PRODUCCION
//string UserID = "AAPE880825DD2";
//string UserPass = "38c5256f5533ba27d26b80539cc18d7922df4627";
```

### Contraseñas por Empresa
```csharp
if (this.Contrato == "ca2be778-3ba5-48f3-8fae-00b831faebea")//TIIM
    this.Contraseña = "Fuckoo06!";
```

---

## 📌 Notas Importantes

1. **Timeout**: Configurado a 600,000 ms (10 minutos)
2. **Fecha Ajustada**: Se resta 1 hora a la fecha para compensar zona horaria
3. **Redondeo**: Preciso a 2 decimales para montos, 4 para importes unitarios
4. **Base64**: Observaciones deben codificarse en Base64
5. **CSV**: Separador de campos es pipe (|), separador de líneas es \n
6. **Objeto Impuesto**: Siempre "02" (Sí objeto de impuesto)

---

## 🔗 Referencias

- **PADE (PAC)**: Proveedor Autorizado de Certificación
- **SAT**: Servicio de Administración Tributaria
- **CFDI**: Comprobante Fiscal Digital por Internet
- **RFC**: Registro Federal de Contribuyentes
- **UUID**: Folio Fiscal (identificador único del comprobante)

---

## 📧 Contacto

**Desarrollado por**: TIIM Tecnología  
**Email**: pavel_tiim@hotmail.com / tiimfacturacion@hotmail.com  
**Website**: www.tiimtecnologia.com  
**Teléfono Soporte**: 668-101-3253 (Gerente Admin. Anabel Araujo)

---

## 📅 Versión del Documento

- **Fecha**: 2024
- **Versión CFDI**: 4.0
- **Versión Complemento de Pago**: 2.0
- **Framework**: .NET Framework 4.8
- **C#**: 7.3

---

## ⚠️ Advertencias

- Este archivo es para **PRUEBAS**. No usar en producción sin cambiar credenciales.
- Validar siempre los cálculos de impuestos con el área contable.
- Respaldar archivos XML y PDF generados.
- Mantener logs de errores para auditoría.
- No compartir credenciales de PAC.

---

*Documento generado automáticamente por GitHub Copilot*
