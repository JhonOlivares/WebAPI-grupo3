using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIGrupo3.Models;

namespace WebAPIGrupo3.DBService
{
    public class FirestoreConn
    {
        readonly FirestoreDb db;

        public FirestoreConn()
        {
            db = FirestoreDb.Create("grupo3webapi");
        }

        //get single doc from database
        public async Task<DocumentSnapshot> GetSingleDocumentAsync(string collection, string docID)
        {
            DocumentReference docRef = db.Collection(collection).Document(docID);
            DocumentSnapshot DocumentSnap = await docRef.GetSnapshotAsync();
            if (DocumentSnap.Exists)
            {
                return DocumentSnap;
            }
            return null;
        }

        //get all documents from a collection
        public async Task<IReadOnlyList<DocumentSnapshot>> GetCollectionAsync(string collection)
        {
            CollectionReference alldocs = db.Collection(collection);
            QuerySnapshot snapshot = await alldocs.GetSnapshotAsync();
            return snapshot.Documents;
        }

        //get documents from a collection filter by property(WhereEcualTo)
        public async Task<IReadOnlyList<DocumentSnapshot>> GetCollectionAsync(string collection, string propiedad, string filterValue)
        {
            CollectionReference docs = db.Collection(collection);
            Query qu = docs.WhereEqualTo(propiedad, filterValue);
            QuerySnapshot snapshot = await qu.GetSnapshotAsync();
            return snapshot.Documents;
        }

        //guardar en la base de datos cualquier objeto con el atributo [FirestoreData]
        //0:exception
        //1: guardado en db
        //2: guardado con random docID;
        //3: no se guardó porque el docID ya existe en la base de datos.
        public async Task<string> UploadDocToFirestore(string collection, object obj, string CustomID)
        {
            try
            {
                DocumentReference docRef;
                if (!string.IsNullOrEmpty(CustomID))
                {
                    docRef = db.Collection(collection).Document(CustomID);
                    DocumentSnapshot snap = await docRef.GetSnapshotAsync();
                    if (snap.Exists)
                    {
                        return snap.Id;//no se va sobreescribir el documento, porque ya existe: CustomID.
                    }
                    await docRef.SetAsync(obj);
                    return ""; ;// el documento se guardó correctamente con un id=CustomID
                }
                else
                {
                    docRef = await db.Collection(collection).AddAsync(obj);
                    //await docRef.Collection.add(obj);
                    return docRef.Id;// el documento se guardó correctamente con un id aleatorio
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public async Task<Dictionary<string, object>> GetLastDocId(string collection)
        {
            try
            {
                DocumentReference docRef = db.Collection("last_doc_id").Document(collection);
                DocumentSnapshot snap = await docRef.GetSnapshotAsync();
                if (snap.Exists)
                {
                    return snap.ToDictionary();
                }
            }
            catch (Exception)
            {
                return new Dictionary<string, object>();
            }
            return new Dictionary<string, object>();
        }

        //actualizar un documento: argumento "docfields" los valores a agregar en el documento existente
        public async Task<byte> UpdateGenericDocumentField(string collection, string documentId, Dictionary<string, object> dicFields)
        {
            try
            {
                DocumentReference dr = db.Collection(collection).Document(documentId);
                await dr.UpdateAsync(dicFields);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //verificar si existe un documento: 1 si existe, 2 no existe, 0 exception
        public async Task<int> CheckDocument(string collection, string document)
        {
            try
            {
                DocumentReference docRef = db.Collection(collection).Document(document);
                DocumentSnapshot snap = await docRef.GetSnapshotAsync();
                if (snap.Exists)
                {
                    return 1;
                }
                else
                    return 2;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
