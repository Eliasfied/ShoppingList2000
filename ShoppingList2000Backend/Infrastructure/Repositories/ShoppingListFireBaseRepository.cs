using Application.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Google.Cloud.Firestore;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ShoppingListFireBaseRepository : IShoppingListRepository
    {
        IMapper _mapper;
        private readonly FirestoreDb _firestoreDb;
        private const string _collectionName = "ShoppingList";

        public ShoppingListFireBaseRepository(IMapper mapper, FirestoreDb firestoreDb)
        {
            _mapper = mapper;
            _firestoreDb = firestoreDb;
        }

        public async Task<ShoppingList> CreateShoppingList(ShoppingList shoppingList)
        {
            var collection = _firestoreDb.Collection(_collectionName);
            var shoppingDocument = _mapper.Map<ShoppingListDocument>(shoppingList);

             var shoppingDocumentBackReference = await collection.AddAsync(shoppingDocument);

            var shoppingListBack = await GetShoppingList(shoppingDocumentBackReference.Id);

            return shoppingListBack;

        }
        public async Task<ShoppingList> GetShoppingList(string shoppingListId)
        {
            var documentReference = _firestoreDb.Collection(_collectionName).Document(shoppingListId);
            var snapshot = await documentReference.GetSnapshotAsync();
            var shoppingListDocument = snapshot.ConvertTo<ShoppingListDocument>();
            var shoppingList = _mapper.Map<ShoppingList>(shoppingListDocument);
            return shoppingList;
            
        }
        public async Task<List<ShoppingList>> GetAllShoppingLists(string userId)
        {
            var collection = _firestoreDb.Collection(_collectionName);
            var creatorQuery = collection.WhereEqualTo("CreatorUserId", userId);
            var eligibleUsersQuery = collection.WhereArrayContains("EligibleUsers", userId);
            var creatorSnapshot = await creatorQuery.GetSnapshotAsync();
            var eligibleUsersSnapshot = await eligibleUsersQuery.GetSnapshotAsync();

            var shoppingLists = new List<ShoppingList>();

            shoppingLists.AddRange(creatorSnapshot.Documents
                .Select(document => _mapper.Map<ShoppingList>(document.ConvertTo<ShoppingListDocument>())));

            shoppingLists.AddRange(eligibleUsersSnapshot.Documents
                .Select(document => _mapper.Map<ShoppingList>(document.ConvertTo<ShoppingListDocument>())));

            return shoppingLists;
        }

        public async Task<ShoppingList> UpdateShoppingList(ShoppingList shoppingList)
        {
            var documentReference = _firestoreDb.Collection(_collectionName).Document(shoppingList.ShoppingListId);
            var shoppingListDocument = _mapper.Map<ShoppingListDocument>(shoppingList);

            await documentReference.SetAsync(shoppingListDocument, SetOptions.Overwrite);

            return await GetShoppingList(shoppingList.ShoppingListId);
        }

        public async Task<string> DeleteShoppingList(string shoppingListId)
        {
            var documentReference =  _firestoreDb.Collection(_collectionName).Document(shoppingListId.ToString());

            await documentReference.DeleteAsync();

            return " Item mit der Id: " + shoppingListId + "wurde erfolgreich aus der Datenbank gelöscht!";
        }
    }
}
