using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// This is a WebServer-Cacher.
/// </summary>
[Serializable]
public class Cacher
{
    // data cache. Instance-wide.
    private System.Data.DataTable thecache = null;
    private Int32 rowCardinality = default(Int32);

    // Constructor. Receives a DataTable and assumes it's the cache.
    public Cacher(System.Data.DataTable dt)
    {
        try
        {
            this.thecache = dt;
            this.rowCardinality = this.thecache.Rows.Count;
        }
        catch (System.Exception ex)// the provided DataTable might be null;
        {
            // Logging
            string m = ex.Message;
            thecache = null;
        }
        finally
        {
        }
    }// end BuildCache



    public int GetRowsInCache()
    {
        return this.rowCardinality;
    }


    /// <summary>
    /// navigate forward of a chunk
    /// </summary>
    /// <param name="howMany">number of rows in a chunk</param>
    /// <param name="lastBurnIndex">the last index, consumed on the previous chunk, while navigating forward</param>
    /// <returns>the chunk-datatable</returns>
    public System.Data.DataTable GetNextChunk(int howMany, ref int lastBurnIndex)
    {
        if (null == this.thecache || 0 == this.thecache.Rows.Count)
            return null;
        System.Data.DataTable tmp = new System.Data.DataTable("tmp");// chunk-table
        tmp.Columns.Add("RowCounter", typeof(Int32));// build record-layout in the result
        foreach (System.Data.DataColumn col in this.thecache.Columns)
            tmp.Columns.Add(col.ColumnName, col.DataType);// build record-layout in the result
        //
        int start = lastBurnIndex + 1;
        int stop = lastBurnIndex + howMany;
        for (int c = start; c <= stop; c++)
        {
            if (c < 0)
            {
                c = start = 0;
                stop = howMany - 1;
            }
            if (howMany > this.rowCardinality)
            {
                howMany = this.rowCardinality;
                c = start = 0;
                stop = howMany;
            }
            if (c >= this.rowCardinality)
            {
                stop = this.rowCardinality - 1;// full end reached
                break;
            }
            int columnNumb = this.thecache.Columns.Count;
            object[] tmpRow = new object[columnNumb + 1];
            tmpRow[0] = c + 1;// RowCounter
            for (int curCol = 0; curCol < columnNumb; curCol++)
            {// build record-layout in the copy
                tmpRow[curCol + 1] = this.thecache.Rows[c].ItemArray[curCol];
            }
            tmp.Rows.Add(tmpRow);// add row
        }
        lastBurnIndex = stop;
        return tmp;// return chunk
    }// end GetNextChunk


    /// <summary>
    /// navigate backwards of a chunk
    /// </summary>
    /// <param name="howMany">number of rows in a chunk</param>
    /// <param name="lastBurnIndex">the first index, consumed on the previous chunk, while navigating backwards</param>
    /// <returns>the chunk-datatable</returns>
    public System.Data.DataTable GetPreviousChunk(int howMany, ref int lastBurnIndex)
    {
        if (null == this.thecache || 0 == this.thecache.Rows.Count)
            return null;
        System.Data.DataTable tmp = new System.Data.DataTable("tmp");// chunk-table
        tmp.Columns.Add("RowCounter", typeof(Int32));// build record-layout in the result
        foreach (System.Data.DataColumn col in this.thecache.Columns)
            tmp.Columns.Add(col.ColumnName, col.DataType);// build record-layout in the result
        //
        int start = lastBurnIndex - 2 * howMany + 1;
        int stop = lastBurnIndex - howMany;
        for (int c = start; c <= stop; c++)
        {
            if (c < 0)
            {
                c = start = 0;
                stop = howMany - 1;
            }
            if (howMany > this.rowCardinality)
            {
                howMany = this.rowCardinality;
                c = start = 0;
                stop = howMany;
            }
            if (c >= this.rowCardinality)
            {
                stop = this.rowCardinality - 1;// full end reached
                break;
            }
            int columnNumb = this.thecache.Columns.Count;
            object[] tmpRow = new object[columnNumb + 1];
            tmpRow[0] = c + 1;// RowCounter
            for (int curCol = 0; curCol < columnNumb; curCol++)
            {// build record-layout in the copy
                tmpRow[curCol + 1] = this.thecache.Rows[c].ItemArray[curCol];
            }
            tmp.Rows.Add(tmpRow);// add row
        }
        lastBurnIndex = stop;
        return tmp;// return chunk
    }// end GetPreviousChunk


    #region IDisposable Members

    public void Dispose()
    {
        this.releaseTable();
    }

    ~Cacher()
    {
        this.releaseTable();
    }

    private void releaseTable()
    {
        if (null != this.thecache)
        {
            this.thecache.Clear();
            this.thecache = null;
        }
    }//
    #endregion

}// end class
