using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using System.Collections;

namespace MyAvaloApp.Models
{
    public class Employe : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    ValidateFirstName();
                    OnPropertyChanged(nameof(FirstName));
                    OnPropertyChanged(nameof(ErrorsForFirstName));  // Ajout de la notification pour les erreurs
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    ValidateLastName();
                    OnPropertyChanged(nameof(LastName));
                    OnPropertyChanged(nameof(ErrorsForLastName));  // Ajout de la notification pour les erreurs
                }
            }
        }

        private string _role;
        public string Role
        {
            get => _role;
            set
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged(nameof(Role));
                }
            }
        }

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public bool HasErrors => _errors.Values.Any(e => e.Any());

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        // Correction : enlever public devant la méthode GetErrors
        IEnumerable INotifyDataErrorInfo.GetErrors(string? propertyName)
        {
            // Si la propriété est null, retourne toutes les erreurs
            if (propertyName == null)
            {
                return _errors.Values.SelectMany(e => e);
            }

            // Retourne les erreurs spécifiques à une propriété donnée
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : Enumerable.Empty<string>();
        }

        public void ValidateFirstName()
        {
            if (string.IsNullOrEmpty(_firstName))
            {
                AddError("FirstName", "Le prénom est obligatoire.");
            }
            else
            {
                ClearError("FirstName");
            }
        }

        public void ValidateLastName()
        {
            if (string.IsNullOrEmpty(_lastName))
            {
                AddError("LastName", "Le nom est obligatoire.");
            }
            else
            {
                ClearError("LastName");
            }
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }
            _errors[propertyName].Add(error);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors[propertyName].Clear();
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        // Propriétés exposées pour les erreurs des champs
        public string ErrorsForFirstName => string.Join(", ", GetErrorsForProperty(nameof(FirstName)));
        public string ErrorsForLastName => string.Join(", ", GetErrorsForProperty(nameof(LastName)));
        public string ErrorsForRole => string.Join(", ", GetErrorsForProperty(nameof(Role)));

        private IEnumerable<string> GetErrorsForProperty(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : Enumerable.Empty<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
